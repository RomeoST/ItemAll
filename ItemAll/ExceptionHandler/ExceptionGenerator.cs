using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ItemAll.Forms.Message;

namespace ItemAll.ExceptionHandler
{
    [Serializable]
    public class FileNotFoundExceptionST : Exception
    {
        private string _fileNotFound { get; set; }
        public FileNotFoundExceptionST() { }
        public FileNotFoundExceptionST(string message) { ShowMessage.Show("Файловая ошибка", message, STTYPE_MESSAGE.TEXT); }
        public FileNotFoundExceptionST(string message, Exception inner) :base(message, inner) { }
        protected FileNotFoundExceptionST(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
