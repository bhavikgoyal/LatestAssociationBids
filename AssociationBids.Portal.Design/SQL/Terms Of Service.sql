
declare @portalKey int = 1,
		@agreementDate datetime = '1/1/2021',
		@agreementTitle varchar(150),
		@agreement nvarchar(max),
		@status int = 1


set @agreementTitle = 'Terms Of Service'
set @agreement = N'<h3>TERMS OF SERVICE</h3>
        <ol>
            <li>
                Welcome to the Association Bids service (the “Service”), owned and operated by Association Bids, LLC (the “Company”).  The following Terms of Use apply when you view or use the Service via our website, API, or any other access point, or by accessing the Service through clicking on the application (the “App”) on your mobile device.  Please review the following terms carefully.  By accessing or using the Service, you signify your agreement to these Terms of Use.  If you do not agree to these Terms of Use, you may not access or use the Service.
            </li>
            <li>
                PRIVACY POLICY
                <ol>
                    <li>
                        The Company respects the privacy of its Service users.  Please refer to the Company’s Privacy Policy found here: associationbids.com/privacy which explains how we collect, use, and disclose information that pertains to your privacy.  When you access or use the Service, you signify your agreement to this Privacy Policy.
                    </li>
                </ol>
            </li>
            <li>
                ABOUT THE SERVICE
                <ol>
                    <li>
                        The Service allows you to visualize and manage your data in ways that should allow you to increase the effectiveness of your management and marketing decisions.  This service also allows you to input user data, obtain marketing leads, submit and be awarded bids, and makes communication with and to your customers and vendors more efficient.
                    </li>
                </ol>
            </li>
            <li>
                REFUNDS
                <ol>
                    <li>
                        All Refund requests must be submitted in writing via email to support@AssociationBids.com. Unless you and The Company agree otherwise in writing, payment obligations cannot be canceled and all fees paid are non-refundable.
                    </li>
                </ol>
            </li>
            <li>
                CANCELLATION
                <ol>
                    <li>
                        To cancel your subscription, contact support@AssociationBids.com. Cancellation requests made in writing are effective immediately. Any charges taking place after a written cancellation request will be refunded. All fees paid prior to the written cancellation request are non-refundable unless you and The Company agree otherwise in writing.
                    </li>
                </ol>
            </li>
            <li>
                REGISTRATION; RULES FOR USER CONDUCT AND USE OF THE SERVICE
                <ol>
                    <li>
                        You need to be at least 18 years old and a resident of the United States to register for and use the Service.
                    </li>
                    <li>
                        If you are a user who signs up for the Service, you will create a personalized account which includes a unique username and a password to access the Service and to receive messages from the Company.  You agree to notify us immediately of any unauthorized use of your password and/or account. The Company will not be responsible for any liabilities, losses, or damages arising out of the unauthorized use of your member name, password and/or account.
                    </li>
                </ol>
            </li>
            <li>
                USE RESTRICTIONS
                <ol>
                    <li>
                        Your permission to use the Site is conditioned upon the following Use Restrictions and Conduct Restrictions: You agree that you will not under any circumstances:
                    </li>
                    <li>
                        Submit a bid for which you are unable or unwilling to honor;
                    </li>
                    <li>
                        Post any information that is abusive, threatening, obscene, defamatory, libelous, or racially, sexually, religiously, or otherwise objectionable and offensive;
                    </li>
                    <li>
                        use the service for any unlawful purpose or for the promotion of illegal activities;
                    </li>
                    <li>
                        attempt to, or harass, abuse or harm another person or group;
                    </li>
                    <li>
                        use another user’s account without permission;
                    </li>
                    <li>
                        provide false or inaccurate information when registering an account;
                    </li>
                    <li>
                        interfere or attempt to interfere with the proper functioning of the Service;
                    </li>
                    <li>
                        make any automated use of the system, or take any action that we deem to impose or to potentially impose an unreasonable or disproportionately large load on our servers or network infrastructure;
                    </li>
                    <li>
                        Engage in unsolicited advertising, marketing or other activities, including without limitation, any activities that violate anti-spamming laws and regulations;
                    </li>
                    <li>
                        bypass any robot exclusion headers or other measures we take to restrict access to the Service or use any software, technology, or device to scrape, spider, or crawl the Service or harvest or manipulate data; or
                    </li>
                    <li>
                        publish or link to malicious content intended to damage or disrupt another user’s browser or computer.
                    </li>
                </ol>
            </li>
            <li>
                POSTING AND CONDUCT RESTRICTIONS
                <ol>
                    <li>
                        When you create your own personalized account, you give us access to your data.  You are solely responsible for the data that you make available to the Service.  By accessing the Service you agree to give the Service total, unrestricted access to any data accessible by the Service.
                    </li>
                    <li>
                        The following rules pertain to data accessed by the Service. By transmitting and submitting any data while using the Service, you agree as follows:
                    </li>
                    <li>
                        You are solely responsible for your account and the activity that occurs while signed in to or while using your account;
                    </li>
                    <li>
                        You will not post data that is malicious, false or inaccurate;
                    </li>
                    <li>
                        You will not submit data that is copyrighted or subject to third party proprietary rights, including privacy, publicity, trade secret, etc., unless you are the owner of such rights or have the appropriate permission from their rightful owner to specifically submit such content; and
                    </li>
                    <li>
                        You hereby affirm we have the right to determine whether any of your data submissions are appropriate and comply with these Terms of Service, remove any and/or all of your submissions, and terminate your account with or without prior notice. This right is not an obligation, and you are still liable for any unlawful submissions if we have not removed it.
                    </li>
                    <li>
                        You understand and agree that any liability, loss or damage that occurs as a result of the use of any data that you make available or access through your use of the Service is solely your responsibility.  The Company is not responsible for any public display or misuse of your data.  The Company does not, and cannot, pre-screen or monitor all user data.  However, at our discretion, we, or technology we employ, may monitor and/or record your interactions with the Service.
                    </li>
                    <li>
                        You agree that you will not engage in any unsolicited advertising, marketing or other activities, including any activities that violate anti-spam laws and regulations including the CAN SPAM Act of 2003, the Telephone Consumer Protection Act, and the Do-Not-Call Implementation Act (or any similar or analogous anti-spam, data protection, or privacy legislation in any other jurisdiction).
                    </li>
                </ol>
            </li>
            <li>
                TCPA COMPLIANCE
                <ol>
                    <li>
                        The Company provides an interactive communication platform to maintain communication with consumers through various means which may include the sending and receiving text/SMS messages. You hereby affirmatively consent for the Service to use any phone number, home business, cellular, or otherwise which you provide to the Service for the solicitation and communication by the Service or its users, even if you are on a federal do not call list. You understand these calls or messages may be generated using an autodialer and may contain pre-recorded messages.
                    </li>
                </ol>
            </li>
            <li>
                CAN-SPAM COMPLIANCE
                <ol>
                    <li>
                        The Company provides an interactive communication platform to maintain communication with consumers through various means which may include the sending and receiving email communications. You hereby affirmatively consent for the Service to use any email address or other digital platform handle or username which you provide to the Service for the solicitation and communication by the Service or its users. You understand these calls or messages may be generated using an autodialer and may contain pre-recorded messages. You may unsubscribe at any time.
                    </li>
                </ol>
            </li>
            <li>
                ONLINE CONTENT DISCLAIMER
                <ol>
                    <li>
                        All testimonials and case studies within this website are, to the best of our ability to determine, true and accurate. They were provided willingly, without any compensation offered in return. These testimonials and case studies do not necessarily represent typical or average results. Most customers do not contact us or offer to share to their results, nor are they required or expected to. Therefore, we have no way to determine what typical or average results might have been.
                    </li>
                    <li>
                        Opinions, advice, statements, offers, or other information or content made available through the Service, but not directly by the Company, are those of their respective authors, and should not necessarily be relied upon.  Such authors are solely responsible for such content.  The Company does not guarantee the accuracy, completeness, or usefulness of any information on the Service and neither does the Company adopt nor endorse, nor is the Company responsible for, the accuracy or reliability of any bid, opinion, advice, or statement made by parties other than the Company.  The Company takes no responsibility and assumes no liability for any data that you or any other user or third-party posts or sends over the Service.  Under no circumstances will the Company be responsible for any loss or damage resulting from anyone’s reliance on information or other content posted on the Service, or transmitted to users.
                    </li>
                    <li>
                        Though the Company strives to enforce these Terms of Use, you may be exposed to content or data that is inaccurate or objectionable.  The Company reserves the right, but has no obligation, to monitor the materials posted in the public areas of the service or to limit or deny a user’s access to the Service or take other appropriate action if a user violates these Terms of Use or engages in any activity that violates the rights of any person or entity or which we deem unlawful, offensive, abusive, harmful or malicious.  The Company shall have the right to remove any such material or data that in its sole opinion violates, or is alleged to violate, the law or this agreement or which might be offensive, or that might violate the rights, harm, or threaten the safety of users or others.  Unauthorized use may result in criminal and/or civil prosecution under Federal, State and local law.  If you become aware of misuse of our Service, please contact us support@AssociationBids.com
                    </li>
                </ol>
            </li>
            <li>
                LINKS TO OTHER SITES AND/OR MATERIALS
                <ol>
                    <li>
                        As part of the Service, the Company may provide you with convenient links to third party website(s) (“Third Party Sites”) as well as content or items belonging to or originating from third parties (the “Third Party Applications, Software or Content”).  These links are provided as a courtesy to Service subscribers.  The Company has no control over Third Party Sites and Third Party Applications, Software or Content or the promotions, materials, information, goods or services available on these Third Party Sites or Third Party Applications, Software or Content.  Such Third Party Sites and Third Party Applications, Software or Content are not investigated, monitored or checked for accuracy, appropriateness, or completeness by the Company, and the Company is not responsible for any Third Party Sites accessed through the Site or any Third Party Applications, Software or Content posted on, available through or installed from the Site, including the content, accuracy, offensiveness, opinions, reliability, privacy practices or other policies of or contained in the Third Party Sites or the Third Party Applications, Software or Content.  Inclusion of, linking to or permitting the use or installation of any Third Party Site or any Third Party Applications, Software or Content does not imply approval or endorsement thereof by the Company.  If you decide to leave the Site and access the Third Party Sites or to use or install any Third Party Applications, Software or Content, you do so at your own risk and you should be aware that our terms and policies no longer govern.  You should review the applicable terms and policies, including privacy and data gathering practices, of any site to which you navigate from the Site or relating to any applications you use or install from the site.
                    </li>
                </ol>
            </li>
            <li>
                COPYRIGHT COMPLAINTS AND COPYRIGHT AGENT
                <ol>
                    <li>
                        Termination of Repeat Infringer Accounts.  The Company respects the intellectual property rights of others and requests that the users do the same.  Pursuant to 17 U.S.C. 512(i) of the United States Copyright Act, the Company has adopted and implemented a policy that provides for the termination in appropriate circumstances of users of the Service who are repeat infringers.  The Company may terminate access for participants or users who are found repeatedly to provide or post protected third party content without necessary rights and permissions.
                    </li>
                    <li>
                        DMCA Take-Down Notices.  If you are a copyright owner or an agent thereof and believe, in good faith, that any materials provided on the Service infringe upon your copyrights, you may submit a notification pursuant to the Digital Millennium Copyright Act (see 17 U.S.C 512) (“DMCA”) by sending the following information in writing to the Company’s designated copyright agent at 3622 W. San Luis Street, Tampa, FL 33629.:
                        <ol>
                            <li>
                                The date of your notification;
                            </li>
                            <li>
                                A physical or electronic signature of a person authorized to act on behalf of the owner of an exclusive right that is allegedly infringed;
                            </li>
                            <li>A description of the copyrighted work claimed to have been infringed, or, if multiple copyrighted works at a single online site are covered by a single notification, a representative list of such works at that site;</li>
                            <li>A description of the material that is claimed to be infringing or to be the subject of infringing activity and information sufficient to enable us to locate such work;</li>
                            <li>Information reasonably sufficient to permit the service provider to contact you, such as an address, telephone number, and/or email address;</li>
                            <li>A statement that you have a good faith belief that use of the material in the manner complained of is not authorized by the copyright owner, its agent, or the law; and</li>
                            <li>A statement that the information in the notification is accurate, and under penalty of perjury, that you are authorized to act on behalf of the owner of an exclusive right that is allegedly infringed.</li>
                        </ol>
                    </li>
                    <li>
                        Counter-Notices. If you believe that your User Content that has been removed from the Site is not infringing, or that you have the authorization from the copyright owner, the copyright owner''s agent, or pursuant to the law, to post and use the content in your User Content, you may send a counter-notice containing the following information to our copyright agent using the contact information set forth above:
                        <ol>
                            <li>
                                Your physical or electronic signature;
                            </li>
                            <li>
                                A description of the content that has been removed and the location at which the content appeared before it was removed;
                            </li>
                            <li>
                                A statement that you have a good faith belief that the content was removed as a result of mistake or a misidentification of the content; and
                            </li>
                            <li>
                                Your name, address, telephone number, and email address, a statement that you consent to the jurisdiction of the federal court in Florida and a statement that you will accept service of process from the person who provided notification of the alleged infringement.
                            </li>
                            <li>If a counter-notice is received by the Company copyright agent, the Company may send a copy of the counter-notice to the original complaining party informing such person that it may reinstate the removed content in 10 business days.  Unless the copyright owner files an action seeking a court order against the content provider, member or user, the removed content may (in the Company’s discretion) be reinstated on the Site in 10 to 14 business days or more after receipt of the counter-notice.</li>
                        </ol>
                    </li>
                </ol>
            </li>
            <li>
                LICENSE GRANT
                <ol>
                    <li>By posting any data via the Service, you expressly grant, and you represent and warrant that you have a right to grant, to the Company a royalty-free, sub licensable, transferable, perpetual, irrevocable, non-exclusive, worldwide license to use, reproduce, modify, publish, list information regarding, edit, translate, distribute, publicly perform, publicly display, and make derivative works of all such data and your name, voice, and/or likeness as contained in your data, if applicable, in whole or in part, and in any form, media or technology, whether now known or hereafter developed, for use in connection with the Service.</li>
                </ol>
            </li>
            <li>
                INTELLECTUAL PROPERTY
                <ol>
                    <li>
                        You acknowledge and agree that we and our licensors retain ownership of all intellectual property rights of any kind related to the Service, including applicable copyrights, trademarks and other proprietary rights.  Other product and company names that are mentioned on the Service may be trademarks of their respective owners. We reserve all rights that are not expressly granted to you under this Agreement.
                    </li>
                </ol>
            </li>
            <li>
                REPRESENTATIONS, WARRANTIES, AND COVENANTS
                <ol>
                    <li>
                        You expressly represent, warrant, and covenant to the Company that you and your business are in compliance with all Federal, State, and Local laws and regulations regarding your marketing and advertising activities. Further, you represent, warrant, and covenant, that any phone number, email, or other contact information which is placed into a marketing package offered by The Company or on to The Company''s severs or system which you use to market to your customers, has met all applicable consent requirements of federal, state and local laws.
                    </li>
                </ol>
            </li>
            <li>
                INDEMNIFICATION
                <ol>
                    <li>
                        You agree to indemnify, defend, save and hold harmless the Company, its directors, officers, employees and agents against and from any and all claims, demands, actions, liabilities, judgments, fines, damages, losses, assessments, penalties, awards and expenses, of any kind or nature whatsoever, including without limitation, attorney’s fees, expert witness fees, and costs of investigation, litigation or dispute resolution, arising out of any breach of these Terms of Service or pertaining in any way to the absence or lack of veracity of express written consent of any person, contact, or number, consumer or the like.
                    </li>
                </ol>
            </li>
            <li>
                EMAIL MAY NOT BE USED TO PROVIDE NOTICE
                <ol>
                    <li>
                        Communications made through the Service’s e-mail and messaging system, will not constitute legal notice to the Company or any of its officers, employees, agents or representatives in any situation where notice to the Company is required by contract or any law or regulation.
                    </li>
                </ol>
            </li>
            <li>
                USER CONSENT TO RECEIVE COMMUNICATIONS IN ELECTRONIC FORM
                <ol>
                    <li>For contractual purposes, you (a) consent to receive communications from the Company in an electronic form via the email address you have submitted; and (b) agree that all Terms of Use, agreements, notices, disclosures, and other communications that the Company provides to you electronically satisfy any legal requirement that such communications would satisfy if it were in writing.  The foregoing does not affect your non-waivable rights.</li>
                    <li>
                        We may also use your email address, to send you other messages, including information about the Company and special offers. You may opt out of such email by changing your account settings or sending an email to support@AssociationBids.com  or mail to the following postal address:
                        <br />
                        Customer Support
                        <br />
                        Association Bids.com
                        <br />
                        3622 W. San Luis Street
                        <br />
                        Tampa, FL 33629
                    </li>
                </ol>
            </li>
            <li>
                Opting out may prevent you from receiving messages regarding the Company or special offers.
            </li>
            <li>
                WARRANTY DISCLAIMER
                <ol>
                    <li>
                        THE SERVICE, IS PROVIDED “AS IS,” WITHOUT WARRANTY OF ANY KIND. WITHOUT LIMITING THE FOREGOING, THE COMPANY EXPRESSLY DISCLAIMS ALL WARRANTIES, WHETHER EXPRESS, IMPLIED OR STATUTORY, REGARDING THE SERVICE INCLUDING WITHOUT LIMITATION ANY WARRANTY OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE, SECURITY, ACCURACY AND NON-INFRINGEMENT. WITHOUT LIMITING THE FOREGOING, THE COMPANY MAKES NO WARRANTY OR REPRESENTATION THAT ACCESS TO OR OPERATION OF THE SERVICE WILL BE UNINTERRUPTED OR ERROR FREE. YOU ASSUME FULL RESPONSIBILITY AND RISK OF LOSS RESULTING FROM YOUR DOWNLOADING AND/OR USE OF FILES, INFORMATION, CONTENT OR OTHER MATERIAL OBTAINED FROM THE SERVICE. SOME JURISDICTIONS LIMIT OR DO NOT PERMIT DISCLAIMERS OF WARRANTY, SO THIS PROVISION MAY NOT APPLY TO YOU.
                    </li>
                </ol>
            </li>
            <li>
                LIMITATION OF DAMAGES; RELEASE
                <ol>
                    <li>
                        TO THE EXTENT PERMITTED BY APPLICABLE LAW, IN NO EVENT SHALL THE COMPANY, ITS AFFILIATES, DIRECTORS, OR EMPLOYEES, OR ITS LICENSORS OR PARTNERS, BE LIABLE TO YOU FOR ANY LOSS OF PROFITS, USE,  OR DATA, OR FOR ANY INCIDENTAL, INDIRECT, SPECIAL, CONSEQUENTIAL OR EXEMPLARY DAMAGES, HOWEVER ARISING, THAT RESULT FROM (A) THE USE, DISCLOSURE, OR DISPLAY OF YOUR USER CONTENT; (B) YOUR USE OR INABILITY TO USE THE SERVICE; (C) THE SERVICE GENERALLY OR THE SOFTWARE OR SYSTEMS THAT MAKE THE SERVICE AVAILABLE; OR (D) ANY OTHER INTERACTIONS WITH THE COMPANY OR ANY OTHER USER OF THE SERVICE, WHETHER BASED ON WARRANTY, CONTRACT, TORT (INCLUDING NEGLIGENCE) OR ANY OTHER LEGAL THEORY, AND WHETHER OR NOT THE COMPANY HAS BEEN INFORMED OF THE POSSIBILITY OF SUCH DAMAGE, AND EVEN IF A REMEDY SET FORTH HEREIN IS FOUND TO HAVE FAILED OF ITS ESSENTIAL PURPOSE.  SOME JURISDICTIONS LIMIT OR DO NOT PERMIT DISCLAIMERS OF LIABILITY, SO THIS PROVISION MAY NOT APPLY TO YOU.
                    </li>
                </ol>
            </li>
            <li>
                MODIFICATION OF TERMS OF USE
                <ol>
                    <li>
                        We can amend these Terms of Use at any time and will revise the “Last Updated” date above in the event of any such amendments.  It is your sole responsibility to check the Site from time to time to view any such changes in the Agreement.  If you continue to use the Site, you signify your agreement to our revisions to these Terms of Use. Any changes to these Terms (other than as set forth in this paragraph) or waiver of the Company’s rights hereunder shall not be valid or effective except in a written agreement bearing the physical signature of an officer of the Company.  No purported waiver or modification of this Agreement by the Company via telephonic or email communications shall be valid.
                    </li>
                </ol>
            </li>
            <li>
                GENERAL TERMS
                <ol>
                    <li>
                        If any part of this Agreement is held invalid or unenforceable, that portion of the Agreement will be construed consistent with applicable law.  The remaining portions will remain in full force and effect. Any failure on the part of the Company to enforce any provision of this Agreement will not be considered a waiver of our right to enforce such provision.  Our rights under this Agreement will survive any termination of this Agreement.
                    </li>
                    <li>
                        You agree that any cause of action related to or arising out of your relationship with the Company must commence within ONE year after the cause of action accrues.  Otherwise, such cause of action is permanently barred.
                    </li>
                    <li>
                        These Terms of Use and your use of the Site are governed by the federal laws of the United States of America and the laws of the State of Florida, without regard to conflict of law provisions.
                    </li>
                    <li>
                        The Company may assign or delegate these Terms of Service and/or the Company’s Privacy Policy, in whole or in part, to any person or entity at any time with or without your consent. You may not assign or delegate any rights or obligations under the Terms of Service or Privacy Policy without the Company’s prior written consent, and any unauthorized assignment and delegation by you is void.
                    </li>
                </ol>
                
            </li>
            <li>
                YOU ACKNOWLEDGE THAT YOU HAVE READ THESE TERMS OF USE, UNDERSTAND THE TERMS OF USE, AND WILL BE BOUND BY THESE TERMS AND CONDITIONS. YOU FURTHER ACKNOWLEDGE THAT THESE TERMS OF USE TOGETHER WITH THE PRIVACY POLICY AT   WWW.ASSOCIATIONBIDS.COM/COMPANY/PRIVACY-POLICY REPRESENT THE COMPLETE AND EXCLUSIVE STATEMENT OF THE AGREEMENT BETWEEN US AND THAT IT SUPERSEDES ANY PROPOSAL OR PRIOR AGREEMENT ORAL OR WRITTEN, AND ANY OTHER COMMUNICATIONS BETWEEN US RELATING TO THE SUBJECT MATTER OF THIS AGREEMENT.
            </li>
        </ol>
    </div>'

if not exists (
	select 1
	from [Agreement]
	where PortalKey = @portalKey
	and Title = @agreementTitle
	and AgreementDate = @agreementDate
)
begin

	insert into Agreement (PortalKey, Title, AgreementDate, [Description], LastModificationTime, [Status])
		
	values (@portalKey, @agreementTitle, @agreementDate, @agreement, GETDATE(), @status)

end