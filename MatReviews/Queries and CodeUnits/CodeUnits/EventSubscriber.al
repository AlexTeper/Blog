codeunit 50102 EventSubscriber
{
    trigger OnRun()
    begin

    end;

    [EventSubscriber(ObjectType::Table, Database::Author, 'OnAfterInsertEvent', '', true, true)]
    local procedure WelcomeMessage(var Rec: Record Author)
    begin
        Message('Welcome dear %1', Rec.Name);
    end;



    [EventSubscriber(ObjectType::Table, Database::Book, 'OnAfterInsertEvent', '', true, true)]
    local procedure BooksMessage(var Rec: Record Book)
    var
        joinQuery: Query JoinQuery;

    begin
        // joinQuery.SetFilter(AuthorName, '=%1', Rec.AuthorName);
        if joinQuery.Open() then begin
            while joinQuery.Read() do begin
                //Message(joinQuery.BookTitle);
                Message(Format(joinQuery.PageCount));
            end;
            joinQuery.Close();
        end;
    end;
}