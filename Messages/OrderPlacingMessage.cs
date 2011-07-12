using System.Collections.Generic;

namespace Messages
{
    public class OrderPlacingMessage
    {
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public IList<long> item_ids { get; set; }
    }
}
