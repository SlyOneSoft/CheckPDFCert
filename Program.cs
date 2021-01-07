using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using iText.Signatures;
using iText.Kernel.Pdf;

namespace CheckPDFCert
{
	class Program
	{



		static void Verify()
		{
			String digestAlgorithm = "";
			String encryptionAlgorithm = "";
			DateTime signDate;
			bool verifyTimeStamp = false;
			Org.BouncyCastle.X509.X509Certificate signCert;
			bool revokeStatus = false;
			bool isTSP = false;

			PdfDocument pdfDocument = new PdfDocument(new PdfReader("c:\\temp\\valid signed contract.pdf"));

			// Checks that signature is genuine and the document was not modified.
			Boolean genuineAndWasNotModified = false;

			String signatureFieldName = "Signature1";
			SignatureUtil signatureUtil = new SignatureUtil(pdfDocument);
			try
			{
				PdfPKCS7 signature1 = signatureUtil.ReadSignatureData(signatureFieldName);
				if (signature1 != null)
				{
					genuineAndWasNotModified = signature1.VerifySignatureIntegrityAndAuthenticity();

					digestAlgorithm = signature1.GetDigestAlgorithm();
					encryptionAlgorithm = signature1.GetEncryptionAlgorithm();
					signDate = signature1.GetTimeStampDate();
					verifyTimeStamp = signature1.VerifyTimestampImprint();
					signDate = signature1.GetSignDate();
					signCert = signature1.GetSigningCertificate();
					isTSP = signature1.IsTsp();
					revokeStatus = signature1.IsRevocationValid();

				}
			}
			catch (iText.Signatures.VerificationException issue)
			{
				issue.
			}
			catch (Exception ignored)
			{
				// ignoring exceptions,
				// we are only interested in signatures that are passing the check successfully
			}

			pdfDocument.Close();
		}

	

	static void coversAllDocument()

	{ 

		PdfDocument pdfDocument = new PdfDocument(new PdfReader("c:\\temp\\valid signed contract.pdf"));

		String signatureFieldName = "Signature1";
		SignatureUtil signatureUtil = new SignatureUtil(pdfDocument);

		Boolean completeDocumentIsSigned = signatureUtil.SignatureCoversWholeDocument(signatureFieldName);
		if (!completeDocumentIsSigned)
		{
			// handle PDF file which contains NOT signed data
		}
 
		pdfDocument.Close();
  
    }

		void oldVerify()
		{
			bool hasCert = false;
			bool verifiedCert = false;

			// Load PDF document
			using (Document pdfDocument = new Document("c:\\temp\\valid signed contract.pdf"))
			{

				using (PdfFileSignature signature = new PdfFileSignature(pdfDocument))
				{
					IList<string> sigNames = signature.GetSignNames();
					if (sigNames.Count > 0) // Any signatures?
					{
						verifiedCert = signature.VerifySignature(sigNames[0] as string);
						if (signature.VerifySigned(sigNames[0] as string)) // Verify first one
						{
							if (signature.IsCertified) // Certified?
							{
								if (signature.GetAccessPermissions() == DocMDPAccessPermissions.FillingInForms) // Get access permission
								{
									// Do something
								}
							}
						}
					}
				}
			}


		}

		static void Main(string[] args)
		{
			Verify();



		}


	}






}
