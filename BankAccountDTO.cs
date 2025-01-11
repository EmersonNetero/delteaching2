namespace delteaching
{
    public class BankAccountDTO
    {
        public string branch {  get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string holderName { get; set; }
        public string holderEmail { get; set; }
        public string holderDocument { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set;}
    }
}
