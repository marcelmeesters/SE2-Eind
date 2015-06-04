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
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav">
            <li id="menu_default"><a href="/">Home</a></li>
            <li id="menu_films" class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Films<span class="caret"></span></a>
              <ul class="dropdown-menu" role="menu">
                <li><a href="/films/actueel">Actueel</a></li>
                <li><a href="/films/verwacht">Verwacht</a></li>
                <li><a href="/films/archief">Archief</a></li>
              </ul>
            </li>
              <asp:LoginView ID="AdminMenuViewHeader" runat="server">
                      <RoleGroups>
                          <asp:RoleGroup Roles="GlobalAdmin,UserAdmin,BlogAdmin,FilmAdmin">
                              <ContentTemplate>
                                  <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Admin<span class="caret"></span></a>
                                      <ul class="dropdown-menu" role="menu">
                                          <li><a href="/Admin">Dashboard</a></li>
                              </ContentTemplate>
                          </asp:RoleGroup>
                        </RoleGroups>
                  </asp:LoginView>
              <asp:LoginView ID="AdminMenuViewContent" runat="server">
                      <RoleGroups>
                          <asp:RoleGroup Roles="GlobalAdmin">
                              <ContentTemplate>
                                  <li><a href="/Admin/Staff">Medewerkers</a></li>
                                  <li><a href="/Admin/Blog">Blog</a></li>
                                  <li><a href="/Admin/Films">Films</a></li>
                                  <li><a href="/Admin/Cinema">Bioscopen</a></li>
                                  <li><a href="/Admin/Agenda">Bioscoopagenda</a></li>
                                  <li><a href="/Admin/Users">Gebruikers</a></li>
                              </ContentTemplate>
                          </asp:RoleGroup>
                          <asp:RoleGroup Roles="BlogAdmin,GlobalAdmin">
                              <ContentTemplate>
                                  <li><a href="/Admin/Blog">Blog</a></li>
                              </ContentTemplate>
                          </asp:RoleGroup>
                          <asp:RoleGroup Roles="FilmAdmin,GlobalAdmin">
                              <ContentTemplate>
                                <li><a href="/Admin/Films">Films</a></li>
                                <li><a href="/Admin/Cinema">Bioscopen</a></li>
                                <li><a href="/Admin/Agenda">Bioscoopagenda</a></li>
                              </ContentTemplate>
                          </asp:RoleGroup>
                          <asp:RoleGroup Roles="UserAdmin,GlobalAdmin">
                              <ContentTemplate>
                                <li class="dropdown">
                                 <li><a href="/Admin/Users">Gebruikers</a></li>
                              </ContentTemplate>
                          </asp:RoleGroup>
                          </RoleGroups>
                  </asp:LoginView>
               <asp:LoginView ID="AdminMenuViewFooter" runat="server">
                      <RoleGroups>
                          <asp:RoleGroup Roles="GlobalAdmin,UserAdmin,BlogAdmin,FilmAdmin">
                              <ContentTemplate>
                                   </ul>
                                </li>
                              </ContentTemplate>
                          </asp:RoleGroup>
                      </RoleGroups>
          </asp:LoginView>
              <li>
                  <asp:LoginView ID="LoginViewMenu" runat="server">
              <AnonymousTemplate>
                  <a href="/Login">Log in</a>
              </AnonymousTemplate>
              <LoggedInTemplate>
                 <a><asp:LoginName ID="LoginNameMenu" runat="server" /></a>
              </LoggedInTemplate>
          </asp:LoginView>
              </li>
          </ul>
        </div>
      </div>
    </nav>
