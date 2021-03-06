﻿using NHibernate.Cfg; // .DatabaseIntegration extension method


using System.Linq;

namespace DomainMapping
{
    public static class Mapper
    {

        static NHibernate.ISessionFactory _sessionFactory = Mapper.BuildSessionFactory();


        // call this on production
        public static NHibernate.ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }
        

        public static NHibernate.ISessionFactory BuildSessionFactory(bool useUnitTest = false)
        {
            var mapper = new NHibernate.Mapping.ByCode.ConventionModelMapper();


            mapper.IsEntity((t, declared) => t.Namespace == "Domain");

            mapper.BeforeMapClass += mapper_BeforeMapClass;
            mapper.BeforeMapProperty += mapper_BeforeMapProperty;
            mapper.BeforeMapManyToOne += mapper_BeforeMapManyToOne;            
            mapper.BeforeMapBag += mapper_BeforeMapBag;

            var cfg = new NHibernate.Cfg.Configuration();



            // .DatabaseIntegration! Y U EXTENSION METHOD?!
            cfg.DataBaseIntegration(c =>
            {
                var cs = System.Configuration.ConfigurationManager.ConnectionStrings["TheSpaConnection"].ConnectionString;


                // SQL Server
                c.Driver<NHibernate.Driver.SqlClientDriver>();
                c.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
                c.ConnectionString = "Server=.;Database=SpaArchitectureMvp;Trusted_Connection=True";

                //// PostgreSQL                
                //c.Driver<NHibernate.Driver.NpgsqlDriver>();
                //c.Dialect<NHibernate.Dialect.PostgreSQLDialect>();
                //c.ConnectionString = cs;

                if (useUnitTest)
                {
                    c.LogSqlInConsole = true;
                    c.LogFormattedSql = true;
                }
            });


            System.Collections.Generic.IEnumerable<System.Type> entities = typeof(Domain.PersonDomain.Person).Assembly.GetExportedTypes()
                .Where(x => !(x.IsAbstract && x.IsSealed)); // exclude static 



            NHibernate.Cfg.MappingSchema.HbmMapping mapping = mapper.CompileMappingFor(entities);

            cfg.AddMapping(mapping);


            // http://www.ienablemuch.com/2013/06/multilingual-and-caching-on-nhibernate.html
            //var filterDef = new NHibernate.Engine.FilterDefinition("lf", /*default condition*/ null,
            //                                           new Dictionary<string, NHibernate.Type.IType>
            //                                                           {
            //                                                               { "LanguageCultureCode", NHibernate.NHibernateUtil.String}
            //                                                           }, useManyToOne: false);
            //cfg.AddFilterDefinition(filterDef);



            cfg.Cache(x =>
            {
                // SysCache is not stable on unit testing
                if (!useUnitTest)
                {
                    x.Provider<NHibernate.Caches.SysCache.SysCacheProvider>();

                    // I don't know why SysCacheProvider is not stable on simultaneous unit testing, 
                    // might be SysCacheProvider is just giving one session factory, so simultaneous test see each other caches
                    // This solution doesn't work: http://stackoverflow.com/questions/700043/mstest-executing-all-my-tests-simultaneously-breaks-tests-what-to-do                    
                }
                else
                {
                    // This is more stable in unit testing
                    x.Provider<NHibernate.Cache.HashtableCacheProvider>();
                }


                // http://stackoverflow.com/questions/2365234/how-does-query-caching-improves-performance-in-nhibernate

                // Need to be explicitly turned on so the .Cacheable directive on Linq will work:                    
                x.UseQueryCache = true;
            });



            if (useUnitTest)
                cfg.SetInterceptor(new NHSQLInterceptor());



            //new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(useStdOut: false, doUpdate: true);


            //using (var file = new System.IO.FileStream(@"c:\x\ddl.txt",
            //       System.IO.FileMode.Create,
            //       System.IO.FileAccess.ReadWrite))
            //using (var sw = new System.IO.StreamWriter(file))
            //{
            //    new SchemaUpdate(cfg)
            //        .Execute(sw.Write, false);
            //}


            var sf = cfg.BuildSessionFactory();




            return sf;
        }

        static void mapper_BeforeMapBag(
            NHibernate.Mapping.ByCode.IModelInspector modelInspector, 
            NHibernate.Mapping.ByCode.PropertyPath member, 
            NHibernate.Mapping.ByCode.IBagPropertiesMapper propertyCustomizer)
        {

            /*
             * class Person
             * {
             *      IList<Hobby> Hobbies
             * }
             * 
             * 
             */

            string parentEntity = member.LocalMember.DeclaringType.Name; // this gets the Person
            string foreignKey = parentEntity + "Id";            
            propertyCustomizer.Key(keyMapping => keyMapping.Column(foreignKey));


            // http://www.ienablemuch.com/2014/10/inverse-cascade-variations-on-nhibernate.html
            // best persistence approach: Inverse+CascadeAll 
            propertyCustomizer.Inverse(true);
            propertyCustomizer.Cascade(NHibernate.Mapping.ByCode.Cascade.All);
            propertyCustomizer.Cache(cacheMapping => cacheMapping.Usage(NHibernate.Mapping.ByCode.CacheUsage.ReadWrite));
        }

        

        static void mapper_BeforeMapManyToOne(
            NHibernate.Mapping.ByCode.IModelInspector modelInspector, 
            NHibernate.Mapping.ByCode.PropertyPath member, 
            NHibernate.Mapping.ByCode.IManyToOneMapper propertyCustomizer)
        {
            /*
             
                public class Product
                {
                    protected internal  int                             ProductId       { get; set; }

                    public              TheProduction.ProductCategory   ProductCategory { get; protected internal set; }
                    public              string                          ProductName     { get; protected internal set; }
                }
             
             */

            // ProductCategory property name maps to ProductCategoryId column name
            propertyCustomizer.Column(member.ToColumnName() + "Id");
        }

        static void mapper_BeforeMapProperty(
            NHibernate.Mapping.ByCode.IModelInspector modelInspector,
            NHibernate.Mapping.ByCode.PropertyPath member,
            NHibernate.Mapping.ByCode.IPropertyMapper propertyCustomizer)
        {
            //string postgresFriendlyName = member.ToColumnName().ToLowercaseNamingConvention();
            //propertyCustomizer.Column(postgresFriendlyName);            
        }

        static void mapper_BeforeMapClass(NHibernate.Mapping.ByCode.IModelInspector modelInspector,
            System.Type type,
            NHibernate.Mapping.ByCode.IClassAttributesMapper classCustomizer)
        {

            classCustomizer.Cache(cacheMapping => cacheMapping.Usage(NHibernate.Mapping.ByCode.CacheUsage.ReadWrite));

            string fullName = type.FullName; // example: Domain.TheProduction+Product

            string[] fullNameSplit = fullName.Split('+');

            string className = fullNameSplit[1];

            // Last() skips the other namespace(s)
            string schemaDomainName = fullNameSplit[0].Split('.').Last();

            string schemaName = schemaDomainName.Substring(0, schemaDomainName.Length - "Domain".Length); 

            string sqlServerFullName = schemaName + "." + className;
            classCustomizer.Table(sqlServerFullName);

            System.Reflection.MemberInfo mi = type.GetMember(className + "Id",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)[0];

            classCustomizer.Id(mi,
                idMapper =>
                {
                    idMapper.Column(className + "Id");
                    idMapper.Generator(NHibernate.Mapping.ByCode.Generators.Identity);
                });


        }



        class NHSQLInterceptor : NHibernate.EmptyInterceptor
        {
            // http://stackoverflow.com/questions/2134565/how-to-configure-fluent-nhibernate-to-output-queries-to-trace-or-debug-instead-o
            public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
            {

                Mapper.NHibernateSQL = sql.ToString();
                return sql;
            }

        }

        public static string NHibernateSQL { get; set; }






    } // Mapper


}