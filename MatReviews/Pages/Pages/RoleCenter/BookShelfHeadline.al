page 50106 BookshelfHeadline
{
    PageType = HeadlinePart;


    layout
    {
        area(Content)
        {
            group(HeadLinePart)
            {
                field(headlineOne; headlineOne)
                {
                    ApplicationArea = All;
                }

                field(headlineTwo; headlineTwo)
                {
                    ApplicationArea = All;
                    trigger OnDrillDown()
                    var
                        // DrillDownURLTxt: Label 'https://go.microsoft.com/fwlink/?linkid=867580', Locked = True;
                        Book: Record Book;
                        BookListPage: Page BookList;
                    begin
                        // Hyperlink(DrillDownURLTxt);
                        BookListPage.Run();
                    end;
                }

                field(headLineThree; headLineThree)
                {
                    ApplicationArea = All;
                }
            }
        }
    }

    var
        headlineOne: Label 'This is the headline one';
        headLineTwo: Label '<qualifier>The first headline</qualifier><payload>This is the <emphasize>Headline 1</emphasize>.</payload>';
        headLineThree: Label 'This is headline three';
}