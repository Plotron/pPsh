namespace pPsh.Models
{
    public class ScenarioEmail
    {
        public int ID { get; set; }
        public int ScenarioID { get; set; }
        public virtual Scenario Scenario { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string RecipientAddress { get; set; }
    }
}