page 50104 MyRoleCenter
{
    PageType = RoleCenter;

    layout
    {
        area(RoleCenter)
        {
            part(HeadLinePart; BookshelfHeadline)
            {
                ApplicationArea = All;
            }

            part(BookStatsPart; RoleCenterBookCardPart)
            {
                ApplicationArea = All;
            }
        }
    }

    actions
    {
        area(Sections)
        {
            group(BookShelf)
            {
                Caption = 'My Bookshelf';
                action(BookList)
                {
                    ApplicationArea = All;
                    Caption = 'Book List';
                    RunObject = Page BookList;
                }

                action(AuthorList)
                {
                    ApplicationArea = All;
                    Caption = 'Author List';
                    RunObject = Page AuthorList;
                }
            }
        }

        area(Embedding)
        {
            action(CustomerList)
            {
                ApplicationArea = All;
                Caption = 'Customer List';
                RunObject = Page "Customer List";
            }

            action(ExtensionList)
            {
                ApplicationArea = All;
                Caption = 'Extension List';
                RunObject = Page "Extension Management";
            }
        }

        area(Processing)
        {
            action(Test)
            {
                ApplicationArea = All;
                Caption = 'Test Action';
                RunObject = Page "Customer List";
            }
        }
    }
}

profile BookshelfProfile
{
    Description = 'This is my first profile';
    Caption = 'Bookshelf Profile';
    RoleCenter = MyRoleCenter;
}