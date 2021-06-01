page 50102 AuthorList
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = Author;
    CardPageId = AuthorCard;

    layout
    {
        area(Content)
        {
            repeater(General)
            {
                field(Id; Rec.Id)
                {
                    ApplicationArea = All;
                }

                field(Name; Rec.Name)
                {
                    ApplicationArea = All;
                }

                field(NumberOfBooksWritten; Rec.NumberOfBooksWritten)
                {
                    Caption = 'Number of books written';
                    ApplicationArea = All;
                }
            }
        }
    }


    // Accessing a CodeUnit
    trigger OnOpenPage();
    var
        MyCodeUnit: CodeUnit MyTestCodeUnit;
        ProcedureReturnValue: Decimal;
    begin
        //MyCodeUnit.Run();
        MyCodeUnit.WelcomeMessage('Tyler');
        ProcedureReturnValue := MyCodeUnit.Addition(5, 6);
        Message('The return value was: %1', ProcedureReturnValue);
    end;
}