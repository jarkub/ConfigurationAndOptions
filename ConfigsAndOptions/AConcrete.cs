namespace webapp_my.ConfigsAndOptions
{
    public class AConcrete : IInterface
    {

        // Implement Interface
        //public string? AStringProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public bool ABooleanProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // Implement Interface Explicitly
        string? IInterface.AStringProperty { 
            get; 
            set; 
        }
        public string? AStringProperty { 
            get { return ((IInterface)this).AStringProperty; }
            set { ((IInterface)this).AStringProperty = value; }
        }
        public bool ABooleanProperty { 
            get { return ((IInterface)this).ABooleanProperty; } 
        }
        bool IInterface.ABooleanProperty { 
            get; 
            set; 
        }

        public bool IsNull2()
        {
            Console.WriteLine($"{nameof(AConcrete)}.{nameof(IsNull2)}() called");
            /*
             * IInterface iface = this as IInterface;
             * // NEVER CALLS IInterface.IsNull2()!!!
             * return iface.IsNull2(); // STACK OVERFLOW
             */
            return string.IsNullOrEmpty(((IInterface)this).AStringProperty);
        }

        public bool IsNotNull => !((IInterface)this).IsNull2();
        //string.IsNullOrEmpty(AStringProperty) == false;
        //((IInterface)this).IsNotNull;
    }
}
