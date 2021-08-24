
declare @portalKey int = 1,
		@agreementDate datetime = '1/1/2021',
		@agreementTitle varchar(150),
		@agreement nvarchar(max),
		@status int = 1


set @agreementTitle = 'Privacy Policy'
set @agreement = N'<h3>Association Bids Privacy Policy</h3>
        <p>Last Updated: January 2021</p>
        <ol>
            <li>This privacy policy has been compiled to better serve those who are concerned with how their ''Personally identifiable information'' (PII) is being used online. PII, as used in US privacy law and information security, is information that can be used on its own or with other information to identify, contact, or locate a single person, or to identify an individual in context. Please read our privacy policy carefully to get a clear understanding of how we collect, use, protect or otherwise handle your Personally Identifiable Information in accordance with our website.</li>
            <li>
                What personal information do we collect from the people that visit our website or app?
                <ol>
                    <li>When ordering or registering on our site, as appropriate, you may be asked to enter your name, email address, mailing address, phone number, credit card information, insurance and licensing information or other details to help you with your experience.</li>
                </ol>
            </li>
            <li>
                When do we collect information?
                <ol>
                    <li>We collect information from you when you register on our site, place an order, respond to a survey, onboard information, win or award a bid, fill out a form or enter information on our site. We also collect information on our service platform that you input related to your customers. However, we do not share or use such third-party information unless otherwise instructed by you or them.</li>
                </ol>
            </li>
            <li>
                How do we use your information?
                <ol>
                    <li>
                        We may use the information we collect from you in the following ways:
                        <ol>
                            <li>To personalize user''s experience and to allow us to deliver the type of content and product offerings in which you are most interested.</li>
                            <li>To improve our website, database, or application in order to better serve you.</li>
                            <li>To allow us to better service you in responding to your customer service requests.</li>
                            <li>To administer a contest, promotion, survey or other site feature.</li>
                            <li>To quickly process your transactions.</li>
                            <li>To send periodic emails, text messages, notifications, and communications regarding your transactions or other products and services.</li>
                        </ol>
                    </li>
                </ol>
            </li>
            <li>
                How do we protect visitor information?
                <ol>
                    <li>Our website is scanned on a regular basis for security holes and known vulnerabilities in order to make your visit to our site as safe as possible.</li>
                    <li>We use regular Malware Scanning.</li>
                    <li>Your personal information is contained behind secured networks and is only accessible by a limited number of persons who have special access rights to such systems, and are required to keep the information confidential. In addition, all sensitive/credit information you supply is encrypted via Secure Socket Layer (SSL) technology.</li>
                    <li>We implement a variety of security measures when a user places or awards a bid, enters, submits, or accesses their information to maintain the safety of your personal information.</li>
                    <li>All transactions are processed through a gateway provider and are not stored or processed on our servers.</li>
                </ol>
            </li>
            <li>
                Do we use ''cookies''?
                <ol>
                    <li>We may. Cookies are small files that a site or its service provider transfers to your computer''s hard drive through your Web browser (if you allow) that enables the site''s or service provider''s systems to recognize your browser and capture and remember certain information. For instance, we use cookies to help us remember and process the items in your input forms. They are also used to help us understand your preferences based on previous or current site activity, which enables us to provide you with improved services. We may also use cookies to help us compile aggregate data about site traffic and site interaction so that we can offer better site experiences and tools in the future.</li>
                    <li>
                        We may use cookies to:
                        <ol>
                            <li>Help remember and process orders.</li>
                            <li>Understand and save user''s preferences for future visits.</li>
                            <li>Keep track of advertisements.</li>
                            <li>Compile aggregate data about site traffic and site interactions in order to offer better site experiences and tools in the future. We may also use trusted third-party services that track this information on our behalf.</li>
                        </ol>
                    </li>
                    <li>
                        You can choose to have your computer warn you each time a cookie is being sent, or you can choose to turn off all cookies. You do this through your browser (like Internet Explorer) settings. Each browser is a little different, so look at your browser''s Help menu to learn the correct way to modify your cookies.
                        <ol>
                            <li>If you disable cookies, some features will be disabled, It could affect the user''s experience and some of our services may not function properly.</li>
                        </ol>
                    </li>
                </ol>
            </li>
            <li>
                Third-party disclosure
                <ol>
                    <li>We do not sell, trade, or otherwise transfer to outside parties your personally identifiable information unless we provide users with advance notice. This does not include website hosting partners and other parties who assist us in operating our website, conducting our business, or serving our users, so long as those parties agree to keep this information confidential. We may also release information when it''s release is appropriate to comply with the law, enforce our site policies, or protect ours or others'' rights, property, or safety. However, non-personally identifiable visitor information may be provided to other parties for marketing, advertising, or other uses.</li>
                </ol>
            </li>
            <li>
                Third-party links
                <ol>
                    <li>Occasionally, at our discretion, we may include or offer third-party products or services on our website. These third-party sites have separate and independent privacy policies. We therefore have no responsibility or liability for the content and activities of these linked sites. Nonetheless, we seek to protect the integrity of our site and welcome any feedback about these sites.</li>
                </ol>
            </li>
            <li>
                California Online Privacy Protection Act
                <ol>
                    <li>
                        CalOPPA is the first state law in the nation to require commercial websites and online services to post a privacy policy. The law''s reach stretches well beyond California to require a person or company in the United States (and conceivably the world) that operates websites collecting personally identifiable information from California consumers to post a conspicuous privacy policy on its website stating exactly the information being collected and those individuals with whom it is being shared, and to comply with this policy.<br />
                        See more at: <a href="http://consumercal.org/california-online-privacy-protection-act-caloppa/#sthash.0FdRbT51.dpuf" target="_blank">http://consumercal.org/california-online-privacy-protection-act-caloppa/#sthash.0FdRbT51.dpuf</a>
                        <br />
                        <br />
                        According to CalOPPA we agree to the following:
                    </li>
                    <li>Users can visit our site anonymously.</li>
                    <li>Once this privacy policy is created, we will add a link to it on our home page or as a minimum on the first significant page after entering our website.</li>
                    <li>Our Privacy Policy link includes the word ''Privacy'' and can be easily be found on the page specified above.</li>
                    <li>Users will be notified of any privacy policy changes:</li>
                    <li>
                        On our Privacy Policy Page
                        <ol>
                            <li>
                                Users are able to change their personal information:
                            </li>
                            <li>By emailing us</li>
                        </ol>
                    </li>
                    <li>
                        How does our site handle do not track signals?
                        <ol>
                            <li>We honor do not track signals and do not track, plant cookies, or use advertising when a Do Not Track (DNT) browser mechanism is in place.</li>
                        </ol>
                    </li>
                    <li>
                        Does our site allow third-party behavioral tracking?
                        <ol>
                            <li>It''s also important to note that we allow third-party behavioral tracking</li>
                        </ol>
                    </li>
                    <li>
                        COPPA (Children Online Privacy Protection Act)
                        <ol>
                            <li>
                                When it comes to the collection of personal information from children under 13, the Children''s Online Privacy Protection Act (COPPA) puts parents in control. The Federal Trade Commission, the nation''s consumer protection agency, enforces the COPPA Rule, which spells out what operators of websites and online services must do to protect children''s privacy and safety online. We do not specifically market to children under 13.
                            </li>
                        </ol>
                    </li>
                </ol>
            </li>
            <li>
                Fair Information Practices
                <ol>
                    <li>
                        The Fair Information Practices Principles form the backbone of privacy law in the United States and the concepts they include have played a significant role in the development of data protection laws around the globe. Understanding the Fair Information Practice Principles and how they should be implemented is critical to comply with the various privacy laws that protect personal information.
                    </li>
                    <li>
                        In order to be in line with Fair Information Practices we will take the following responsive action, should a data breach occur:
                        <ol>
                            <li>We will notify the users via email</li>
                            <li>
                                Within 7 business days
                            </li>
                            <li>
                                We also agree to the Individual Redress Principle, which requires that individuals have a right to pursue legally enforceable rights against data collectors and processors who fail to adhere to the law. This principle requires not only that individuals have enforceable rights against data users, but also that individuals have recourse to courts or government agencies to investigate and/or prosecute non-compliance by data processors.
                            </li>
                        </ol>
                    </li>
                </ol>
            </li>
            <li>
                Contacting Us<br />
                If there are any questions regarding this privacy policy you may contact us using the information below.<br />
                Association Bids, LLC<br />
                support@associationbids.com
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