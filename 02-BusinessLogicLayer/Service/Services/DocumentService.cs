﻿using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.DocumentDTO;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DocumentDTO> AddDocumentAsync(DocumentDTO dto)
        {
            var document = _mapper.Map<Document>(dto);
            await _unitOfWork.Repository<Document, int>().AddAsync(document);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<DocumentDTO>(document);
        }

        public async Task<List<DocumentDTO>> GetAllAsync()
        {
            var documents = await _unitOfWork.Repository<Document, int>().GetAllAsync();
            return _mapper.Map<List<DocumentDTO>>(documents);
        }

        public async Task<DocumentDTO> GetDocumentByIdAsync(int id)
        {
            var document = await _unitOfWork.Repository<Document, int>().GetByIdAsync(id);
            return document == null ? null : _mapper.Map<DocumentDTO>(document);
        }

        public async Task<DocumentDTO> UpdateDocumentAsync(DocumentDTO dto)
        {
            var document = await _unitOfWork.Repository<Document, int>().GetByIdAsync(dto.DocumentId);
            if (document == null) throw new Exception("Document not found");

            _mapper.Map(dto, document);

            await _unitOfWork.Repository<Document, int>().UpdateAsync(document);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DocumentDTO>(document);
        }

        public async Task<bool> DeleteDocumentAsync(DocumentDTO dto)
        {
            var document = _mapper.Map<Document>(dto);
            var result = await _unitOfWork.Repository<Document, int>().DeleteAsync(document);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteDocumentByIdAsync(int id)
        {
            var result = await _unitOfWork.Repository<Document, int>().DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> ExistsDocumentAsync(int id)
        {
            return await _unitOfWork.Repository<Document, int>().ExistsAsync(d => d.DocumentId == id);
        }



        public async Task<bool> VerifyDocumentAsync(int id)
        {
            var document = await _unitOfWork.Repository<Document, int>().GetByIdAsync(id);
            if (document == null) return false;

            document.IsVerified = true;
            await _unitOfWork.Repository<Document, int>().UpdateAsync(document);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<int> CountDocumentAsync()
        {
            return await _unitOfWork.Repository<Document, int>().CountAsync();
        }

        public async Task AddDocumentsAsync(List<DocumentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var document = _mapper.Map<Document>(dto);
                await _unitOfWork.Repository<Document, int>().AddAsync(document);
            }
            await _unitOfWork.CompleteAsync();
        }



        ///****************************** CloudinaryService ************************************////////////

        public async Task<DocumentDTO> UpdateLinkOnlyAsync(int documentId, string newFilePath, DocumentType newDocumentType)
        {
            var repo = _unitOfWork.Repository<Document, int>();

            var document = await repo.GetByIdAsync(documentId);

            if (document == null)
                throw new Exception("document not found");

            document.FilePath = newFilePath;
            document.DocumentType = newDocumentType;

            await repo.UpdateAsync(document);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DocumentDTO>(document);
        }

        public async Task<List<DoctorDocumentsDTO>> GetDocumentsByDoctorIdAsync(int doctorId)
        {
            var documents = await _unitOfWork.Repository<Document, int>().GetAllAsync(new QueryOptions<Document>
            {
                Filter = d => d.DoctorId == doctorId
            });

            if (documents == null)
                throw new Exception($"No documents found for doctor with ID {doctorId}.");
            return _mapper.Map<List<DoctorDocumentsDTO>>(documents);

        }
    }
}
