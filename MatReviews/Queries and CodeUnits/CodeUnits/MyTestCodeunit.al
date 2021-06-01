codeunit 50100 MyTestCodeUnit
{
    trigger OnRun()
    begin
        Message('Hello World');
    end;


    // Void függvény
    procedure WelcomeMessage(Name: Text)
    begin
        Message('Hello %1', Name);
    end;


    // Returns a Decimal value called Result
    procedure Addition(FirstNumber: Decimal; SecondNumber: Decimal) Result: Decimal
    begin
        Result := FirstNumber + SecondNumber;
        Result := Result * 2;
    end;

    procedure BooksMessage()
    var
        joinQuery: Query JoinQuery;
    begin
        // joinQuery.SetFilter(AuthorCode, '=%1', Rec.Author);
        if joinQuery.Open() then begin
            while joinQuery.Read() do begin
                //Message(joinQuery.BookTitle);
                Message(Format(joinQuery.PageCount));
            end;
            joinQuery.Close();
        end;
    end;
}