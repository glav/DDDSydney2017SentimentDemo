﻿using Glav.CognitiveServices.FluentApi.Core;
using Glav.CognitiveServices.FluentApi.Core.Communication;
using Glav.CognitiveServices.FluentApi.TextAnalytic.Domain.ApiResponses;
using System;

namespace Glav.CognitiveServices.FluentApi.TextAnalytic.Domain
{
    public sealed class LanguagesResult : BaseResponseResult<LanguagesResultResponseRoot>
    {
        public LanguagesResult(ICommunicationResult apiCallResult)
        {
            ApiCallResult = apiCallResult;
            AddResultToCollection();
        }

        private void AddResultToCollection()
        {
            if (ApiCallResult == null)
            {
                ResponseData = new LanguagesResultResponseRoot { errors = new ApiErrorResponse[] { new ApiErrorResponse { id = 1, message = "No data returned." } } };
                ActionSubmittedSuccessfully = false;
                return;
            }

            try
            {
                ResponseData = Newtonsoft.Json.JsonConvert.DeserializeObject<LanguagesResultResponseRoot>(ApiCallResult.Data);
                if (ResponseData == null || ResponseData.documents == null || ResponseData.errors != null && ResponseData.errors.Length > 0)
                {
                    ActionSubmittedSuccessfully = false;
                    return;
                }
                ActionSubmittedSuccessfully = true;
            } catch (Exception ex)
            {
                ResponseData = new LanguagesResultResponseRoot { errors = new ApiErrorResponse[] { new ApiErrorResponse { id = 1, message = $"Error parsing results: {ex.Message}" } } };
                ActionSubmittedSuccessfully = false;
            }
        }

    }


}
