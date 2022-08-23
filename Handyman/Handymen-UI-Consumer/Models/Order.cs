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
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Service Name")]
            public string? ServiceName { get; set; }
            [Required]
            [DataType(DataType.MultilineText)]
            [Display(Name = "Service Description")]
            public string? Description { get; set; }
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
            public DateTimeOffset Date { get; set; }
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
            public int ServiceId { get; internal set; }
             public string? ServiceImageUrl { get;  set; }
            public bool IsTracking 
            {
                get { return tracking; }
                set { tracking = value; } 
            }
                //[DataType(DataType.DateTime)]
                //[Display(Name = "Date Modified")]


                //public DateTimeOffset DateModified
                //{
                //    get 
                //    { 
                //        return dateModified;
                //    }
                //    set 
                //    {
                //        if (value != this.dateModified)
                //        {
                //            dateModified = value;
                //            NotifyPropertyChanged();
                //        }

                //    }
                //}

                public bool IsConfirmed 
                {
                    get { return isConfirmed; }
                    set { isConfirmed = value; }
                }
    }
}
