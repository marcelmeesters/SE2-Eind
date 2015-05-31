<!-- Fixed navbar -->
    <nav class="navbar navbar-default navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="/">Pathé</a>
        </div>
        <div id='navbar' class='navbar-collapse collapse'>
          <ul class='nav navbar-nav'>
            <li id="menu_default"><a href='/'>Home</a></li>
            <li id="menu_films" class='dropdown'>
            <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Films<span class='caret'></span></a>
              <ul class='dropdown-menu' role='menu'>
                <li><a href='/films/actueel'>Actueel</a></li>
                <li><a href='/films/verwacht'>Verwacht</a></li>
                <li><a href='/films/archief'>Archief</a></li>
              </ul>
            </li>
            <li class='dropdown'>
              <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Admin<span class='caret'></span></a>
              <ul class='dropdown-menu' role='menu'>
                <li><a href='/partners/admin/general'>General Settings</a></li>
                <li><a href='/partners/admin/automation'>Automation Settings</a></li>
                <li><a href='/partners/admin/users'>User Management</a></li>
                <li><a href='/partners/admin/users/new'>New User</a></li>
                <li><a href='/partners/admin/promos'>Promotions</a></li>
                <li><a href='/partners/admin/links'>Links</a></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>