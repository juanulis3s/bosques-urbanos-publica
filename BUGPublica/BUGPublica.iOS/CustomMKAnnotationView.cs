using MapKit;
namespace BUGPublica.iOS
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {
            Id = id;
        }
    }
}
