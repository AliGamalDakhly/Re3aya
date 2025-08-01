﻿using _01_DataAccessLayer.Enums;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.DocumentDTO;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IDocumentService
    {
        Task<DocumentDTO> AddDocumentAsync(DocumentDTO dto);
        Task AddDocumentsAsync(List<DocumentDTO> dtos);
        Task<bool> DeleteDocumentByIdAsync(int id);
        Task<bool> DeleteDocumentAsync(DocumentDTO dto);
        Task<DocumentDTO> UpdateDocumentAsync(DocumentDTO dto);
        Task<List<DocumentDTO>> GetAllAsync();
        Task<DocumentDTO> GetDocumentByIdAsync(int id);
        Task<int> CountDocumentAsync();
        Task<bool> ExistsDocumentAsync(int id);
        Task<bool> VerifyDocumentAsync(int id);

        Task<DocumentDTO> UpdateLinkOnlyAsync(int documentId, string newFilePath, DocumentType newDocumentType);
        Task<List<DoctorDocumentsDTO>> GetDocumentsByDoctorIdAsync(int doctorId);
    }
}
