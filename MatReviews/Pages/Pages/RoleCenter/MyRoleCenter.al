page 50105 MyRoleCenter
{
    PageType = RoleCenter;

    layout
    {
        area(RoleCenter)
        {

            part(HeadlinePart; BookshelfHeadline)
            {
                ApplicationArea = All;
            }
            part(MyPart; BookCardPart)
            {
                Caption = 'Book statistics';
                ApplicationArea = All;
                // SubPageView = SORTING (Author.);
            }
        }

    }

    actions
    {
        area(Sections)
        {
            group(Bookshelf)
            {
                Caption = 'Bookshelf';
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

        area(Processing)
        {
            action(CustomerLista)
            {
                ApplicationArea = All;
                Caption = 'Customer Lista';
                RunObject = Page "Customer List";
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
    }
}

profile BookshelfProfile
{
    Description = 'Profile for managing books';
    Caption = 'BookshelfProfile';
    RoleCenter = MyRoleCenter;
}