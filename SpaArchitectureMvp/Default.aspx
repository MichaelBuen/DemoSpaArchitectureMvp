﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SpaArchitectureMvp.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Lazy-loading controllers on AngularJS</title>
    

    <!--<script src="Scripts/angular.min.js"></script>-->
    <!--<script src="Scripts/angular-ui-router.min.js"></script>

    <script src="Scripts/angular-animate.js"></script>

    <script src="Scripts/angular-ui/ui-bootstrap-tpls.js"></script>


    <script src="Scripts/showdown.min.js"></script>-->




    <link href="/Content/bootstrap.min.css" rel="stylesheet" />  


    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />

        
    <link rel="stylesheet" href="/Themes/Theme.css" />

    <script src="/Scripts/require.js"></script>


    <script src="/App/RequireMain.js"></script>




    
</head>

<body ng-cloak>


    <div id="outer-container">
        <header>
            <h1>Header {{2 + 3}}</h1>
        </header>
        <div style="clear: both">
        </div>
        <nav>
            <h1>
                <a ui-sref="home" href="#/">Home</a>
                <a ui-sref="board" href="#/board">Board</a>
                <a ui-sref="product" href="#/product">Product</a>
            </h1>
        </nav>

        <div style="clear: both">
        </div>

        <section ui-view>           
            {{1+2}}
        </section>

        <aside>
            Sidebar
        </aside>
        
        <div style="clear: both">
        </div>

        <footer>
            <h1>Footer</h1>
        </footer>
    </div>



    <pre>
      <!-- Here's some values to keep an eye on in the sample in order to understand $state and $stateParams -->
      $state = {{$state.current.name}}
      $stateParams = {{$stateParams}}
      $state full url = {{ $state.$current.url.source }}
      <!-- $state.$current is not a public api, we are using it to
           display the full url for learning purposes-->        
    </pre>




    

</body>


</html>
