namespace UniverseSso.Models.Implementation
{
    public class LoginFieldModel
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string OptionalFieldValues { get; set; }
        public bool Required { get; set; }
        public string Pattern { get; set; }
    }
}
