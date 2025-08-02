namespace _02_BusinessLogicLayer.DTOs.AccountDTOs
{
    public class RegisterDTO
    {

        public string UserName { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsDoctor { get; set; }

    }
}
