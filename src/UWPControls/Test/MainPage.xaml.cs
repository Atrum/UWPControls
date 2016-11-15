using System;
using System.Reactive.Linq;
using System.Threading;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var context = SynchronizationContext.Current;
            Loaded += (sender, args) =>
            {
                Observable.Interval(TimeSpan.FromMilliseconds(50))
                    //.Select(i => i % 100)
                    .Where(i => i <= 100)
                    .Subscribe(i =>
                    {
                        context.Post(o =>
                        {
                            CircularProgressBar.Value = (i*0.01);
                        },null);
                    });
            };
        }
        
    }
    
    
}
