codeunit 50101 InstallAuthorCodeUnit
{
    Subtype = Install;

    trigger OnInstallAppPerCompany();

    begin
        InsertAuthor(1, 'Jordan Peterson');
        InsertAuthor(2, 'Stephen Hawking');
        InsertAuthor(3, 'Dan Brown');
    end;

    local procedure InsertAuthor(Id: Integer; Name: Text[150])
    var
        AuthorRecord: Record Author;
    begin
        if AuthorRecord.IsEmpty then begin
            AuthorRecord.Id := Id;
            AuthorRecord.Name := Name;
            AuthorRecord.Insert();
        end;
    end;


}