using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        private readonly ISpecializationService _specialzationService;
        private readonly IDoctorTimeSlotService _doctorTimeSlotService;
        private readonly IRatingService _ratingService;
        private AddressDTO addressDto;

        public DoctorService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,


            IDoctorTimeSlotService doctorTimeSlotService, IMapper mapper,


            IRatingService ratingService,
            IAddressService addressService, ISpecializationService specializationService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _addressService = addressService;
            _specialzationService = specializationService;
            _doctorTimeSlotService = doctorTimeSlotService;
            _ratingService = ratingService;
        }




        public async Task<bool> ApproveDoctorAccountAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Status = DoctorAccountStatus.Approved;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> SuspendDoctorAccountAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Status = DoctorAccountStatus.Suspended;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> RejectDoctorAccountAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Status = DoctorAccountStatus.Rejected;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> PendingDoctorAccountAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Status = DoctorAccountStatus.Pending;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeactivatedDoctorAccountAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Status = DoctorAccountStatus.Deactivated;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }


        public async Task<int> CountDoctorsAsync()
        {
            return await _unitOfWork.Repository<Doctor, int>().CountAsync();
        }

        public async Task<bool> DeleteDoctorByIdAsync(int id)
        {
            var result = await _unitOfWork.Repository<Doctor, int>().DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> ExistsDoctorAsync(int id)
        {
            return await _unitOfWork.Repository<Doctor, int>().ExistsAsync(d => d.DoctorId == id);
        }

        public async Task<List<DoctorCardDTO>> GetAllAsync()
        {
            List<Doctor> doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync(


                new QueryOptions<Doctor>
                {
                    Includes = [d => d.Addresses, d => d.Specialization, d => d.AppUser, d => d.Documents],
                    OrderBy = d => d.RatingValue,


                    SortDirection = SortDirection.Descending,
                    Filter = d => d.Status == DoctorAccountStatus.Approved
                });


            List<DoctorCardDTO> doctorsDtos = _mapper.Map<List<DoctorCardDTO>>(doctors);

            //foreach(var doctor in doctors)
            //{
            //    CityDTO city = await _addressService.GetCityByIdAsync(doctor.Addresses.FirstOrDefault()?.CityId ?? 0);
            //    doctorsDtos.FirstOrDefault(d => d.DoctorId == doctor.DoctorId).GovernemntId = city?.GovernmentId ?? 0;



            //}


            for (int i = 0; i < doctors.Count; i++)


            {
                CityDTO city = await _addressService.GetCityByIdAsync(doctors[i].Addresses.FirstOrDefault()?.CityId ?? 0);
                doctorsDtos[i].GovernemntId = city?.GovernmentId ?? 0;

                bool hasAvailableTimeSlots = await _doctorTimeSlotService.HasAvailableTimeSlots(doctors[i].DoctorId);
                bool hasAvailableTimeSlotsToday = await _doctorTimeSlotService.HasAvailableTimeSlots(doctors[i].DoctorId, DateTime.Now);
                bool hasAvailableTimeSlotsTomorrow = await _doctorTimeSlotService.HasAvailableTimeSlots(doctors[i].DoctorId, DateTime.Now.Date.AddDays(1));

                doctorsDtos[i].HasAvailableTimeSlots = hasAvailableTimeSlots;
                doctorsDtos[i].HasAvailableTimeSlotsToday = hasAvailableTimeSlotsToday;
                doctorsDtos[i].HasAvailableTimeSlotsTomorrow = hasAvailableTimeSlotsTomorrow;
            }

            return doctorsDtos;
        }

        public async Task<List<DoctorSmallInfoDto>> GetAllDoctorsAsync()
        {
            List<Doctor> doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync(


                new QueryOptions<Doctor>
                {
                    Includes = [d => d.AppUser, d => d.Documents],
                    OrderBy = d => d.AppUser.CreatedAt,


                    SortDirection = SortDirection.Descending

                });
            var result = _mapper.Map<List<DoctorSmallInfoDto>>(doctors); ;

            //foreach (var doctor in doctors)
            //{
            //    var user = await _userManager.FindByIdAsync(doctor.AppUserId);
            //    var dto = _mapper.Map<DoctorGetDTO>(doctor);
            //    dto.FullName = user.FullName;
            //    dto.Email = user.Email;
            //    dto.PhoneNumber = user.PhoneNumber;
            //    dto.ProfilePictureUrl =
            //    result.Add(dto);
            //}

            return result;
        }



        public async Task<DoctorDetialsDTO> GetDoctorDetailsByIdAsync(int id)
        {
            List<Doctor> doctors = await _unitOfWork.Repository<Doctor, int>().
                GetAllAsync(new QueryOptions<Doctor>
                {
                    Filter = d => d.DoctorId == id,
                    Includes = [d => d.Addresses, d => d.Specialization, d => d.AppUser, d => d.Documents]
                });

            Doctor doctor = doctors.FirstOrDefault();

            if (doctor == null) return null;

            return _mapper.Map<DoctorDetialsDTO>(doctor);
        }


        public async Task<DoctorGetDTO> GetDoctorByIdAsync(int id)
        {
            var doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync(
                new QueryOptions<Doctor>
                {
                    Filter = d => d.DoctorId == id,
                    Includes = [d => d.Addresses, d => d.Specialization, d => d.AppUser, d => d.Documents]
                });
            if (doctors == null) return null;

            var doctor = doctors.FirstOrDefault();
            var user = await _userManager.FindByIdAsync(doctor.AppUserId);

            var address = doctor.Addresses.FirstOrDefault();
            var cityId = address?.CityId ?? 0;
            GovernmentDTO government;
            if (cityId != 0)
            {
                government = await _addressService.GetGovernmentByCityIdAsync(cityId);
            }
            else
            {
                government = new GovernmentDTO
                {
                    GovernmentId = 0,
                    Name = null
                };
            }


            var dto = _mapper.Map<DoctorGetDTO>(doctor);
            dto.Specialization = doctor.Specialization?.Name; // Assuming Specialization is a navigation property
            dto.SpecializationId = doctor.Specialization?.SpecializationId ?? 0; // Assuming SpecializationId is a property in Doctor
            dto.ServiceId = doctor.Service; // Assuming ServiceId is a property in Doctor
            dto.FullName = user.FullName;
            dto.Email = user.Email;
            dto.PhoneNumber = user.PhoneNumber;
            dto.Gender = user.Gender.ToString();


            dto.CityId = doctor.Addresses.FirstOrDefault()?.CityId ?? 0; // Assuming Addresses is a collection and CityId is a property
            dto.CityName = doctor.Addresses.FirstOrDefault()?.City?.Name; // Assuming Addresses is a collection and City is a navigation property

            dto.GovernmentId = government.GovernmentId; // Assuming Addresses is a collection and GovernmentId is a property
            dto.GovernmentName = government.Name; // Assuming Addresses is a collection and Government is a navigation property


            dto.location = doctor.Addresses?.FirstOrDefault()?.Location; // Assuming Addresses is a collection and Location is a property
            dto.DetailedAddress = doctor.Addresses?.FirstOrDefault()?.DetailedAddress; // Assuming Addresses is a collection and DetailedAddress is a property
            dto.MedicalLicenseUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.MedicalLicense).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            dto.NationalIdUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.NationalId).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            dto.GraduationCertificateUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.GraduationCertificate).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            dto.ExperienceCertificateUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.ExperienceCertificate).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            dto.ProfilePictureUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.ProfileImage).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property

            return dto;
        }

        public async Task<DoctorGetDTO> UpdateDoctorAsync(int id, DoctorUpdateDTO doctorDto)
        {
            // Get the doctor entity by ID
            var doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync(
                new QueryOptions<Doctor>
                {
                    Filter = d => d.DoctorId == id,
                    Includes = [d => d.Addresses, d => d.Specialization, d => d.AppUser, d => d.Documents]
                });

            var doctor = doctors.FirstOrDefault();


            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }


            // Map updated fields
            _mapper.Map(doctorDto, doctor); // only non-null fields overwrite entity props


            // Save changes
            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();

            // Get related AppUser for extra data
            var appUser = await _userManager.FindByIdAsync(doctor.AppUserId);

            appUser.FullName = doctorDto.FullName ?? appUser.FullName; // Update full name if provided  
            appUser.Email = doctorDto.Email ?? appUser.Email; // Update email if provided
            appUser.PhoneNumber = doctorDto.PhoneNumber ?? appUser.PhoneNumber; // Update phone number if provided
            appUser.Gender = (Gender)Enum.Parse(typeof(Gender), doctorDto.Gender);
            await _userManager.UpdateAsync(appUser); // Save changes to AppUser



            // Update address if provided

            if (doctorDto.location != null || doctorDto.DetailedAddress != null)
            {
                addressDto = new AddressDTO
                {
                    Location = doctorDto.location, // Assuming location is a string 
                    DetailedAddress = doctorDto.DetailedAddress,// Assuming DetailedAddress is a strin

                    CityId = doctorDto.CityId, // Assuming CityId is an int
                    DoctorId = doctor.DoctorId // Assuming DoctorId is an int
                };
            }
            int address = 0;

            address = doctor.Addresses.FirstOrDefault()?.AddressId ?? 0; // Get the first address ID or 0 if not found    
            if (address != 0)
            {
                await _addressService.UpdateAddressAsync(addressDto, address);
            }
            else
            {
                addressDto = new AddressDTO
                {
                    Location = doctorDto.location, // Assuming location is a string 
                    DetailedAddress = doctorDto.DetailedAddress,// Assuming DetailedAddress is a strin
                    CityId = doctorDto.CityId, // Assuming CityId is an int
                    DoctorId = doctor.DoctorId // Assuming DoctorId is an int
                };
                await _addressService.AddAddressAsync(addressDto); // Add new address if no existing address found
            }



            //await _addressService.UpdateAddressAsync(addressDto, doctor.a);

            GovernmentDTO government;
            if (addressDto.CityId != 0)
            {
                government = await _addressService.GetGovernmentByCityIdAsync(doctorDto.CityId);
            }
            else
            {
                government = new GovernmentDTO
                {
                    GovernmentId = 0,
                    Name = null
                };
            }


            // Map to output DTO
            var output = _mapper.Map<DoctorGetDTO>(doctor);
            output.FullName = appUser.FullName;
            output.Email = appUser.Email;
            output.PhoneNumber = appUser.PhoneNumber;
            output.Gender = appUser.Gender.ToString();
            output.Specialization = _specialzationService.GetByIdAsync(doctorDto.SpecializationId).Result?.Name; // Assuming GetByIdAsync returns a Task<SpecializationDTO>
            output.location = doctorDto.location;
            output.DetailedAddress = doctorDto.DetailedAddress;
            output.ServiceId = doctorDto.Service; // Assuming ServiceId is a property in Doctor
            output.MedicalLicenseUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.MedicalLicense).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            output.NationalIdUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.NationalId).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            output.GraduationCertificateUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.GraduationCertificate).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            output.ExperienceCertificateUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.ExperienceCertificate).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property
            output.ProfilePictureUrl = doctor.Documents.Where(t => t.DocumentType == DocumentType.ProfileImage).Select(t => t.FilePath).FirstOrDefault(); // Assuming Documents is a collection and FilePath is a property

            output.CityId = doctor.Addresses.FirstOrDefault()?.CityId ?? 0; // Assuming Addresses is a collection and CityId is a property
            output.CityName = doctor.Addresses.FirstOrDefault()?.City?.Name; // Assuming Addresses is a collection and City is a navigation property
            output.GovernmentId = government.GovernmentId; // Assuming Addresses is a collection and GovernmentId is a property
            output.GovernmentName = government.Name; // Assuming Addresses is a collection and Government is a navigation property
            return output;
        }



        public async Task<string?> GetDoctorFullNameByIdAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null)
                return null;

            var appUser = await _userManager.FindByIdAsync(doctor.AppUserId);
            if (appUser == null)
                return null;

            return appUser.FullName;
        }


        public async Task UpdateDoctorRating(int doctorId)
        {
            float newRatingVal = await _ratingService.GetDoctorRatingByIdAsync(doctorId);

            Doctor existingDoctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);

            existingDoctor.RatingValue = newRatingVal;
            await _unitOfWork.CompleteAsync();
        }



        ///-------------------------------------------------------------------------

        public async Task<bool> UpdateDoctorBalanceAsync(int doctorId, double amountToAdd)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Balance += amountToAdd;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();

            return true;
        }




        public async Task<List<DTOs.PatientDTOs.AppointmentWithPatientDTO>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment, int>();

            var options = new QueryOptions<Appointment>
            {
                Filter = a => a.DoctorTimeSlot.DoctorId == doctorId,
                Includes = new Expression<Func<Appointment, object>>[]
               {
                    a => a.Patient,
                    a => a.Patient.AppUser,
                    a => a.DoctorTimeSlot,
                    a => a.DoctorTimeSlot.TimeSlot,
                    a => a.DoctorTimeSlot.Doctor,
                    a => a.DoctorTimeSlot.Doctor.AppUser,
                    a => a.DoctorTimeSlot.Doctor.Specialization,
                    a => a.Payment
               }
            };


            var appointments = await appointmentRepo.GetAllAsync(options);
            return _mapper.Map<List<DTOs.PatientDTOs.AppointmentWithPatientDTO>>(appointments);
        }







    }
}
