namespace webapp_my.ConfigsAndOptions
{
    public interface IInterface
    {
        string? AStringProperty { 
            get; 
            set; 
        }
        bool ABooleanProperty { 
            get; 
            set; 
        }

        bool IsNull => string.IsNullOrEmpty(AStringProperty);
        bool IsNull2() {
            Console.WriteLine($"{nameof(IInterface)}.{nameof(IsNull2)}() called");
            return string.IsNullOrEmpty(AStringProperty); 
        }
        //bool IsNull3();

        void SetIsNull1() => ABooleanProperty = string.IsNullOrEmpty(AStringProperty);
        void SetIsNull2() => ABooleanProperty = IsNull;
        void SetIsNull3() { ABooleanProperty = IsNull; }

        bool IsNotNull => 
            !string.IsNullOrEmpty(AStringProperty);
        bool IsNotNull2() { return !string.IsNullOrEmpty(AStringProperty); }
        void SetIsNotNull1() => ABooleanProperty = !string.IsNullOrEmpty(AStringProperty);
        void SetIsNotNull2() => ABooleanProperty = IsNotNull;
        void SetIsNotNull3() { ABooleanProperty = IsNotNull; }
    }
}
