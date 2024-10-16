﻿using Bredinin.TestProject.Service.Core;
using Bredinin.TestProject.Service.Core.Exceptions;

namespace Bredinin.TestProject.Service.Contracts.Exceptions
{
    public enum OwnError
    {
        #region ProductCategory
       
        [ServiceOwnError(message: "Unable to create product category")]
        UnableToCreateProductCategory = 0,

        [ServiceOwnError(message: "Unable to find product category")]
        CanNotFindProductCategory = 1,

        [ServiceOwnError(message: "Unable to update product category")]
        CanNotUpdateProductCategory = 2,

        [ServiceOwnError(message: "Unable to delete product category")]
        CanNotDeleteProductCategory = 3,

        #endregion

        #region Product
        [ServiceOwnError(message: "Unable to create product ")]
        UnableToCreateProduct = 4,

        [ServiceOwnError(message: "Unable to delete product ")]
        CanNotDeleteProduct = 5,

        [ServiceOwnError(message: "Unable to find product ")]
        UnableToFindProduct = 6,

        [ServiceOwnError(message: "Unable to update product ")]
        CanNotUpdateProduct = 7,

        #endregion
    }

}
