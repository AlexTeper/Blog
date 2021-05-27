table 50100 Book
{
    Caption = 'Book';
    DataClassification = ToBeClassified;
    DataCaptionFields = Id, Title, PageCount;

    fields
    {
        field(1; Id; Integer)
        {
            DataClassification = ToBeClassified;
        }

        field(2; Title; Text[100])
        {
            DataClassification = ToBeClassified;

            // NotBlank = true;

            trigger OnValidate();
            begin
                Message('OnValidateTrigger has run');

                if StrLen(Rec.Title) = 0 then begin
                    Error('Title must have a value!');
                end
                else
                    if StrLen(Rec.Title) < 3 then begin
                        Error('Title has to be at least 3 characters long!')
                    end;
                Rec.Title := Rec.Title.ToUpper();
            end;


        }

        field(3; PageCount; Integer)
        {
            DataClassification = ToBeClassified;
            trigger OnValidate()
            begin
                if Rec.PageCount < 10 then begin
                    Error('The page count must be 10 or greater')
                end;

            end;
        }

        field(4; AuthorId; Integer)
        {
            DataClassification = ToBeClassified;
            TableRelation = Author;
        }

        field(5; AuthorName; Text[150])
        {
            TableRelation = Author;
            FieldClass = FlowField;
            Editable = false;
            CalcFormula = lookup(Author.Name where(Id = field(AuthorId)));
        }

        field(6; HoursToRead; Decimal)
        {
            DataClassification = ToBeClassified;
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

        // TestField(Title);
        Validate(Rec.Title);
        Validate(Rec.PageCount);
        Rec.HoursToRead := (Rec.PageCount * 2) / 60;
        /*
        if StrLen(Rec.Title) = 0 then begin
            Error('The title field should not be empty.');
            exit;
        end;*/

    end;

    trigger OnModify()
    begin
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