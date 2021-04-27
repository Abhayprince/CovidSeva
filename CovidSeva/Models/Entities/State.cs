using System.Collections.Generic;

namespace CovidSeva.Models.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public State()
        {

        }
        public State(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static HashSet<State> GetStates()
        {
            var states = new HashSet<State>
            {
                new State(1, "Andaman Nicobar"),
                new State(2, "Andhra Pradesh"),
                new State(3, "Arunachal Pradesh"),
                new State(4, "Assam"),
                new State(5, "Bihar"),
                new State(6, "Chandigarh"),
                new State(7, "Chhattisgarh"),
                new State(8, "Dadra & Nagar Haveli "),
                new State(9, "Daman & Diu"),
                new State(10, "Delhi"),
                new State(11, "Goa"),
                new State(12, "Gujarat"),
                new State(13, "Haryana"),
                new State(14, "Himachal Pradesh"),
                new State(16, "Jammu & Kashmir"),
                new State(17, "Jharkhand"),
                new State(18, "Karnataka"),
                new State(19, "Kerala"),
                new State(20, "Lakshadweep"),
                new State(21, "Madhya Pradesh"),
                new State(22, "Maharashtra"),
                new State(23, "Manipur"),
                new State(24, "Meghalaya"),
                new State(25, "Mizoram"),
                new State(26, "Nagaland"),
                new State(27, "Odisha"),
                new State(28, "Puducherry"),
                new State(29, "Punjab"),
                new State(30, "Rajasthan"),
                new State(31, "Sikkim"),
                new State(32, "Tamil Nadu"),
                new State(33, "Telangana"),
                new State(34, "Tripura"),
                new State(35, "Uttar Pradesh"),
                new State(36, "Uttarakhand"),
                new State(37, "West Bengal"),
            };
            return states;
        }
    }
}