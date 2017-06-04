using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;

namespace WalkingQuest.GUI
{
    class TextEdit: LineEdit
    {

        private const String defaultFont = "Fonts/Font.ttf";
        private const int defaultFontSize = 24;
        private Color defaultColor = new Color(0f, 0f, 0f); // black

        public TextEdit(Context c, Application application) : base(c)
        {
            TextElement.SetColor(defaultColor);
            TextElement.SetFont(application.ResourceCache.GetFont(defaultFont), size: defaultFontSize);
            TextElement.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Center);
        }


    }
}
