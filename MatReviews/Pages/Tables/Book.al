table 50100 Book
{
    DataClassification = ToBeClassified;
    DataCaptionFields = Id, Title, PageCount; // Ezek jelennek meg a Card Pagen

    fields
    {
        field(1; Id; Integer)
        {
            DataClassification = ToBeClassified;
            // AutoIncrement = true; // Bugokat okoz ezért nem használjuk
        }

        field(2; Title; Text[120])
        {
            DataClassification = ToBeClassified;

            trigger OnValidate();
            begin
                //Message('OnValidate has run');
                if StrLen(Rec.Title) = 0 then begin
                    Error('The title must have a value.');
                end
                else
                    if StrLen(Rec.Title) < 3 then begin
                        Error('The title must contain at least 3 characters.');
                    end;
            end;
        }

        field(3; PageCount; Integer)
        {
            DataClassification = ToBeClassified;
        }

        field(4; HoursToRead; Decimal)
        {
            DataClassification = ToBeClassified;
        }

        field(5; AuthorId; Integer) // AuthorId's type must math Author.Id's type
        {
            DataClassification = ToBeClassified;
            TableRelation = Author; // Sets up the table relation
        }

        field(6; AuthorName; Text[150])
        {
            FieldClass = FlowField; // Means that the field is calculated and not stored in the physical table
            // select Author.Name from Author where Author.Id = Book.AuthorId
            CalcFormula = lookup(Author.Name where(Id = field(AuthorId)));
        }

        field(7; NumberOfBooks; Integer)
        {
            FieldClass = FlowField;
            CalcFormula = Count(Book);
        }

        field(8; HardCover; Boolean)
        {
            DataClassification = ToBeClassified;
        }

        field(9; HardCoverBooks; Integer)
        {
            FieldClass = FlowField;
            CalcFormula = Count(Book where(HardCover = const(true)));
        }

        field(10; SumOfHoursToRead; Decimal)
        {
            FieldClass = FlowField;
            CalcFormula = Sum(Book.HoursToRead);
        }
    }

    keys
    {
        key(PK; Id)
        {
            Clustered = true;
        }
    }


    trigger OnInsert()
    begin
        //Message('OnInsert trigger has run');
        Validate(Rec.Title); // Runs the field's OnValidate trigger
        Rec.HoursToRead := (Rec.PageCount * 2) / 60;
    end;

    trigger OnModify()
    begin
        //Message('OnModify trigger has run');
        Rec.HoursToRead := (Rec.PageCount * 2) / 60;
    end;

    trigger OnDelete()
    begin
        Message('OnDelete trigger has run');
    end;

    trigger OnRename()
    begin
        Message('OnRename trigger has run');
    end;

}