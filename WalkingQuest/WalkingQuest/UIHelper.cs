using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Resources;

namespace WalkingQuest
{
    class UIHelper
    {

        private static Color _black = new Color(0f, 0f, 0f);
        private static int defaultFontSize = 24;
        private static String defaultFont = "Fonts/Font.ttf";

        public UIHelper() { }

        // SETS THE TEXT WITHIN THE LINEEDIT UI ELEMENTS ###################################################################

        public static Text setLineEditText(Text text, Application application)
        {

            if (text != null && application != null)
            {
                text.SetColor(_black);
                text.SetFont(application.ResourceCache.GetFont(defaultFont), size: defaultFontSize);

                text.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);

                return text;
            }

            return null;

        }

        public static Text setLineEditText(Text text, Application application, String font, int fontSize, Color color)
        {

            if (text != null && application != null)
            {
                text.SetColor(color);
                text.SetFont(application.ResourceCache.GetFont("Fonts/"+font), size: fontSize);
                text.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
                return text;
            }

            return null;

        }

        // #################################################################################################################


    }
}
