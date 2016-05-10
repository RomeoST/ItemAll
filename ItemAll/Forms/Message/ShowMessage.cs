using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemAll.Forms.Message
{
    public enum STTYPE_MESSAGE
    {
        TEXT,
        INPUT,
        YESNO
    }
    public static class ShowMessage
    {
        public static void Show(string head, string text, STTYPE_MESSAGE type)
        {
            switch(type)
            {
                case STTYPE_MESSAGE.TEXT:
                case STTYPE_MESSAGE.INPUT:
                case STTYPE_MESSAGE.YESNO:
                    new TextMessage(head, text).ShowDialog();
                    break;
            }
        }
    }
}
