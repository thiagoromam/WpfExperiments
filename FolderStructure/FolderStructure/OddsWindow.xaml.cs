using System;

namespace FolderStructure
{
    public partial class OddsWindow
    {
        public OddsWindow()
        {
            InitializeComponent();

            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            ((OddsViewModel) DataContext).Dispose();
        }
    }
}
