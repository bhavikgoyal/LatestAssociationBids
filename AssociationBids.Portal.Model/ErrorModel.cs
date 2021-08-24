using System;

namespace AssociationBids.Portal.Model
{
    public class ErrorModel
    {
        public ErrorModel() { }

        public ErrorModel(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;
        }

        public string Key { get; set; }
        public string ErrorMessage { get; set; }
    }
}

