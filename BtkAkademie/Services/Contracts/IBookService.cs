﻿using Entities.Concrete;
using Entities.DTOs;
using Entities.LinkModels;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges);
        Task<BookDTO> GetOneBookIdAsync(int  id, bool trachChanges);
        Task<BookDTO> CreatOneBookAsync(BookForInsertionDTO bookDto);
        Task UpdateOneBookAsync(int id, BookForUpdateDTO bookDto, bool trachChanges);
        Task DeleteOneBookAsync(int id, bool trachChanges);

        Task<(BookForUpdateDTO bookForUpdateDto,Book book)> GetOneBookForPatchAsync(int id,bool trachChanges);

        Task SaveChangesForPatchAsync(BookForUpdateDTO bookForUpdateDTO,Book book);

        //Versioning
        Task<List<Book>> GetAllBooksAsync(bool trachChanges);

        //İlişkisel Durum icin
        Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trachChanges);
    }
}
