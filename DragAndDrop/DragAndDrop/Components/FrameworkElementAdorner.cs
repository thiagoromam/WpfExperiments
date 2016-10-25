using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DragAndDrop.Components
{
    public class FrameworkElementAdorner : Adorner
    {
        private AdornerLayer _layer;
        private DropType _dropType;

        public FrameworkElementAdorner(UIElement adornedElement) : base(adornedElement)
        {
        }

        public void Update(DropType dropType)
        {
            _dropType = dropType;
            if (_layer == null)
            {
                _layer = AdornerLayer.GetAdornerLayer(AdornedElement);
                _layer.Add(this);
            }

            _layer.Update(AdornedElement);
            Visibility = Visibility.Visible;
        }
        public void Remove()
        {
            Visibility = Visibility.Collapsed;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var adornedElementRect = new Rect(AdornedElement.RenderSize);

            var renderBrush = new SolidColorBrush(Colors.Red) { Opacity = 0.5 };
            var renderPen = new Pen(new SolidColorBrush(Colors.White), 1.5);

            const double renderRadius = 5.0;

            if (_dropType == DropType.Top || _dropType == DropType.Normal)
            {
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            }

            if (_dropType == DropType.Bottom || _dropType == DropType.Normal)
            {
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
            }
        }
    }
}
