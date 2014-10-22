

using DomainMapping;

using NHibernate;
using System;
using UnitTestFriendlyDal;


public static class Common
{

    public static IDomainAccessFactory BuildDomainAccessFactory()
    {
        var daf = new DomainAccessFactory(Common.BuildSessionFactory());
        return daf;
    }

    static ISessionFactory BuildSessionFactory()
    {
        var sf = Mapper.BuildSessionFactory(useUnitTest: true);

        //using (var session = sf.OpenStatelessSession())
        //using (var tx = session.BeginTransaction())
        //{
        //    Console.WriteLine("Stateless update");
        //    var p = session.Get<Person>(1);
        //    p.FirstName = "John";
        //    session.Update(p);
        //    tx.Commit();
        //}


        return sf;
    }
}