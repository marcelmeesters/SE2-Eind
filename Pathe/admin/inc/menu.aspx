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
            <li id="menu_homepage"><a href="/">Homepage</a></li>
            <li id="menu_default"><a href="/Admin">Dashboard</a></li>
            <asp:LoginView ID="AdminMenuFilms" runat="server">
                    <RoleGroups>
                        <asp:RoleGroup Roles="GlobalAdmin,FilmAdmin">
                            <ContentTemplate>
                                <li id="menu_films"><a href="/Admin/Films/List">Films</a></li>
                                <li id="menu_bioscopen"><a href="/Admin/Cinema">Bioscopen</a></li>
                                <li id="menu_agenda"><a href="/Admin/Agenda">Bioscoopagenda</a></li>
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups>
              </asp:LoginView>
              <asp:LoginView ID="AdminMenuBlog" runat="server">
                    <RoleGroups>
                        <asp:RoleGroup Roles="GlobalAdmin,BlogAdmin">
                            <ContentTemplate>
                                <li id="menu_blog"><a href="/Admin/Blog">Blog</a></li>
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups>
              </asp:LoginView>
              <asp:LoginView ID="AdminMenuUsers" runat="server">
                    <RoleGroups>
                        <asp:RoleGroup Roles="GlobalAdmin,UserAdmin">
                            <ContentTemplate>
                                <li id="menu_users"><a href="/Admin/Users">Gebruikers</a></li>
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups>
              </asp:LoginView>
              <asp:LoginView ID="AdminMenuStaff" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="GlobalAdmin">
                        <ContentTemplate>
                            <li id="menu_staff"><a href="/Admin/Staff">Medewerkers</a></li>
                        </ContentTemplate>
                    </asp:RoleGroup>
                    </RoleGroups>
            </asp:LoginView>
          </ul>
        <ul class="nav navbar-nav navbar-right">
            <asp:LoginView ID="LoginViewMenu" runat="server">
                <AnonymousTemplate>
                    <li><a href="/Login">Log in</a></li>
                </AnonymousTemplate>
                <LoggedInTemplate>
                    <li class="dropdown navbar-right">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><asp:LoginName ID="LoginNameMenu" runat="server" /><span class="caret"></span></a>
                    <ul class="dropdown-menu" role="menu">
                        <li><a>Stuff</a></li>
                        <li><a>Things</a></li>
                    </ul>
                    </li>
                 
                </LoggedInTemplate>
            </asp:LoginView>
        </ul>
        </div>
      </div>
    </nav>
