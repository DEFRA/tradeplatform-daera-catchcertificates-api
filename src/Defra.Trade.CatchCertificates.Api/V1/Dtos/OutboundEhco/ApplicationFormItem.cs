// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco
{
    /// <summary>
    /// EHCO definition of a Question and Answer.
    /// </summary>
    public class ApplicationFormItem
    {
        /// <summary>
        /// Identifier for the instance of a Question on an EHCO Form.
        /// </summary>
        public long FormQuestionId { get; set; }

        /// <summary>
        /// Identifier for the Question definition.
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// Name of the Form.
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// Question text asked on the Form.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Answer provided by the User for a Question.
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Order of the Question on a Page.
        /// </summary>
        public int QuestionOrder { get; set; }

        /// <summary>
        /// Page number on a Form.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Occurrence of a Question on a Page where User can add multiple line items.
        /// </summary>
        public int PageOccurrence { get; set; }

        /// <summary>
        /// Scope of the Question.
        /// </summary>
        public string QuestionScope { get; set; }

        /// <summary>
        /// Type of Question.
        /// </summary>
        public string QuestionType { get; set; }
    }
}
