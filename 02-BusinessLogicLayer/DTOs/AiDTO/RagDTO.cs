namespace _02_BusinessLogicLayer.DTOs.AiDTO
{
    public class RagDTO
    {
        public class Availability
        {
            public int Day { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
        }

        public class DoctorResult
        {
            public int DoctorId { get; set; }
            public string Specialization { get; set; }
            public List<Availability> Availability { get; set; }
            public string Text { get; set; }
        }

        public class RagResponse
        {
            public string Summary { get; set; }
            public List<DoctorResult> Results { get; set; }
        }

    }
}
