using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Handymen_UI_Consumer.Models
{
    public class Order 
    {
     private string? status;
     private int orderId;
     private bool tracking;
    //private DateTimeOffset dateModified;
    public event PropertyChangedEventHandler? PropertyChanged;
    private bool isConfirmed;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      

        public Service? ServiceProperty { get; set; } 

        public string? ConsumerID { get; set; }
        
        public string? Stage { get; set; }
        public DateTimeOffset DateFinished { get; set; }
        public int IsAccepted
        {
            get; set;
        }

        //Inprogress / Delivered
        public string? Status
            {
                get
                {
                    return status;
                }
                set
                {
                    if (value != this.status)
                    {
                        status = value;
                        NotifyPropertyChanged();
                    }

                }
            }
            /*prone to change due to conversions*/
            [DataType(DataType.DateTime)]
            [Display(Name = "Date Created")]
            public DateTime DateCreated { get; set; }
            public int Id
            {
                get
                {
                    return orderId;
                }
                set
                {
                    orderId = value;
                }
            }
            
            public bool IsTracking 
            {
                get { return tracking; }
                set { tracking = value; } 
            }


            public bool IsConfirmed 
            {
                get { return isConfirmed; }
                set { isConfirmed = value; }
            }
    }
}
