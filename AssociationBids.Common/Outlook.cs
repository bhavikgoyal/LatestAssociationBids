using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;

namespace AssociationBids.Portal.Common
{
	/// <summary>
	/// Summary description for Outlook.
	/// </summary>
	public class Outlook
	{
		/// <summary>
		/// Indicates the task status.
		/// </summary>
		public enum TaskStatus
		{
			/// <summary>Complete</summary>
			olTaskComplete  = 4,
			/// <summary>Deferred</summary>
			olTaskDeferred  = 8,
			/// <summary>In Progress</summary>
			olTaskInProgress = 2,
			/// <summary>Started</summary>
			olTaskNotStarted = 1,
			/// <summary>Waiting</summary>
			olTaskWaiting = 16
		}

		/// <summary>
		/// Specifies the level of importance for an item marked by the creator of the item.
		/// </summary>
		public enum Importance
		{
			/// <summary>High</summary>
			olImportanceHigh = 4,
			/// <summary>Low</summary>
			olImportanceLow = 1,
			/// <summary>Normal</summary>
			olImportanceNormal = 2
		}

		/// <summary>
		/// Indicates the view mode specified in the CalendarViewMode property of the CalendarView object.
		/// </summary>
		public enum CalendarViewMode
		{
			/// <summary>Displays a 5-day week.</summary>
			olCalendarView5DayWeek = 4, 
			/// <summary>Displays a single day.</summary>
			olCalendarViewDay = 0,
			/// <summary>Displays a month. </summary>
			olCalendarViewMonth = 2 ,
			/// <summary>Displays a number of days equal to the DaysInMultiDayMode property value of the CalendarView object.</summary>
			olCalendarViewMultiDay = 3 ,
			/// <summary>Displays a 7-day week.</summary>			
			olCalendarViewWeek = 1  
		}

		/// <summary>
		/// Specifies on which instance the recurrence will happen.
		/// </summary>
		public enum Instance
		{
			/// <summary>First</summary>
			First = 1,
			/// <summary>Second</summary>
			Second = 2,
			/// <summary>Third</summary>
			Third = 3,
			/// <summary>Fourth</summary>
			Fourth = 4,
			/// <summary>Last</summary>
			Last = 5
		}

		/// <summary>
		/// Specifies the recurrence pattern type.
		/// </summary>
		public enum RecurrenceType
		{
			/// <summary>Daily</summary>
			olRecursDaily = 0,
			/// <summary>Monthly</summary>
			olRecursMonthly = 2,
			/// <summary>Monthly on Nth day of the month</summary>
			olRecursMonthNth = 3,
			/// <summary>Weekly</summary>
			olRecursWeekly = 1,
			/// <summary>Yearly</summary>
			olRecursYearly = 5,
			/// <summary>Yearly on Nth day of the month</summary>
			olRecursYearNth = 6
		}

		/// <summary>
		/// Constants representing days of the week.
		/// </summary>
		public enum DaysOfWeek
		{
			/// <summary>Sunday</summary>
			Sunday = 1,
			/// <summary>Monday</summary>
			Monday = 2,
			/// <summary>Tuesday</summary>
			Tuesday = 4,
			/// <summary>Wednesday</summary>
			Wednesday = 8,
			/// <summary>Thursday</summary>
			Thursday = 16,
			/// <summary>Friday</summary>
			Friday = 32,
			/// <summary>Saturday</summary>
			Saturday = 64,
			/// <summary>Monday, Tuesday, Wednesday, Thursday, Friday</summary>
			Weekday = 62,
			/// <summary>Sunday, Saturday</summary>
			WeekendDay = 65,
			/// <summary>Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday</summary>
			Day = 127
		}

		public Outlook()
		{
			
		}

		
		/// <summary>
		/// Provides custom date manipulations.
		/// Author : Pascal Groulx (2006/11/03)
		/// </summary>
		public class DateUtils
		{		 
			#region Public Property Member...

			/// <summary>Gets recurrence pattern validation error that occured during last validation.</summary>
			public Exception RecurrencePatternException {get{return this._RecurrencePatternException;}}
			/// <summary>Gets/sets maximum occurrence number. Override RecurrencePattern.Occurrences and RecurrencePattern.PatternEndDate</summary>
			public int MaxOccurrence {get{return this._MaxOccurrence;}set{this._MaxOccurrence = value;}}

			#endregion
			#region Private Property Member...

			/// <summary>Start date</summary>
			private DateTime _oStartDate = DateTime.MinValue;
			/// <summary>End date</summary>
			private DateTime _oEndDate = DateTime.MaxValue;
			/// <summary>Stock recurrence pattern validation error.</summary>
			private Exception _RecurrencePatternException = null;
			/// <summary>Override maximum occurence of pattern</summary>
			private int _MaxOccurrence = 1000;

			#endregion

			#region Public Method Member...

			#region Contructor
			#endregion

			/// <summary>
			/// Gets start and end date of last month from current date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>Date range</returns>
			public DateRange GetLastMonth()
			{
				return this.GetLastMonth(DateTime.Now);
			}

			/// <summary>
			/// Gets start and end date of last month from specified date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="poDate">Date</param>
			/// <returns>Date range</returns>
			public DateRange GetLastMonth(DateTime poDate)
			{
				DateTime oDate = poDate.AddMonths(-1);
				this._oStartDate = new DateTime(oDate.Year,oDate.Month,1);
				this._oEndDate = new DateTime(this._oStartDate.Year,this._oStartDate.Month,DateTime.DaysInMonth(this._oStartDate.Year,this._oStartDate.Month));
				return new DateRange(this._oStartDate,this._oEndDate);
			}

			/// <summary>
			/// Gets start and end date of last week from current date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>Date range</returns>
			public DateRange GetLastWeek()
			{
				return this.GetLastWeek(DateTime.Now);
			}

			/// <summary>
			/// Gets start and end date of last week from specified date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="poDate">Date</param>
			/// <returns>Date range</returns>
			public DateRange GetLastWeek(DateTime poDate)
			{
				this._oStartDate = poDate.AddDays((-1 * (int)poDate.DayOfWeek)-7);
				this._oEndDate = this._oStartDate.AddDays(6);
				return new DateRange(this._oStartDate,this._oEndDate);
			}

			/// <summary>
			/// Gets start and end date of current week from current date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>Date range</returns>
			public DateRange GetCurrentWeek()
			{
				return this.GetCurrentWeek(DateTime.Now);
			}

			/// <summary>
			/// Gets start and end date of current week from specified date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="poDate">Date</param>
			/// <returns>Date range</returns>
			public DateRange GetCurrentWeek(DateTime poDate)
			{
				this._oStartDate = poDate.AddDays(-1 * (int)poDate.DayOfWeek);
				this._oEndDate = this._oStartDate.AddDays(6);
				return new DateRange(this._oStartDate,this._oEndDate);
			}

			/// <summary>
			/// Gets start and end date of current month from current date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>Date range</returns>
			public DateRange GetCurrentMonth()
			{
				return this.GetCurrentMonth(DateTime.Now);
			}

			/// <summary>
			/// Gets start and end date of current month from specified date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="poDate">Date</param>
			/// <returns>Date range</returns>
			public DateRange GetCurrentMonth(DateTime poDate)
			{
				this._oStartDate = poDate.AddDays(-1 * (poDate.Day -1));
				this._oEndDate = this._oStartDate.AddDays(DateTime.DaysInMonth(this._oStartDate.Year,this._oStartDate.Month)-1);
				return new DateRange(this._oStartDate,this._oEndDate);
			}

			/// <summary>
			/// Gets start and end date of next week from current date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>Date range</returns>
			public DateRange GetNextWeek()
			{
				return this.GetNextWeek(DateTime.Now);
			}

			/// <summary>
			/// Gets start and end date of next week from specified date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="poDate">Date</param>
			/// <returns>Date range</returns>
			public DateRange GetNextWeek(DateTime poDate)
			{
				this._oStartDate = poDate.AddDays(7 - (int)poDate.DayOfWeek);
				this._oEndDate = this._oStartDate.AddDays(6);
				return new DateRange(this._oStartDate,this._oEndDate);
			}

			/// <summary>
			/// Gets start and end date of next month from current date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>Date range</returns>
			public DateRange GetNextMonth()
			{
				return this.GetNextMonth(DateTime.Now);
			}

			/// <summary>
			/// Gets start and end date of next month from specified date.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="poDate">Date</param>
			/// <returns>Date range</returns>
			public DateRange GetNextMonth(DateTime poDate)
			{
				DateTime oDate = poDate.AddMonths(1);
				this._oStartDate = new DateTime(oDate.Year,oDate.Month,1);
				this._oEndDate = new DateTime(this._oStartDate.Year,this._oStartDate.Month,DateTime.DaysInMonth(this._oStartDate.Year,this._oStartDate.Month));
				return new DateRange(this._oStartDate,this._oEndDate);
			}


			/// <summary>
			/// Gets collection of date created from a pattern.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern</param>
			/// <returns>Return a collection of occurence based on the recurrence pattern.</returns>
			public OccurrenceCollection GetOccurrences(RecurrencePattern pPattern)
			{
				OccurrenceCollection oCollection = null;

				if ( pPattern != null )
				{
					switch ((int)pPattern.RecurrenceType)
					{
						case 0 : oCollection = this.GetDailyRecurrence(pPattern); break;
						case 1 : oCollection = this.GetWeeklyRecurrence(pPattern); break;
						case 2 : oCollection = this.GetMonthlyRecurrence(pPattern); break;
						case 3 : oCollection = this.GetMonthNthRecurrence(pPattern); break;
						case 5 : oCollection = this.GetYearlyRecurrence(pPattern); break;
						case 6 : oCollection = this.GetYearNthRecurrence(pPattern); break;
					}
				}
            
				return oCollection;
			}

			/// <summary>
			/// Gets recurrence text from a recurrence pattern
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence Pattern</param>
			/// <returns>string</returns>
			public string GetRecurrenceText(RecurrencePattern pPattern)
			{
				string Result = "Recurrence pattern is not valid.";
				object[] aParam = null;

				if ( this.ValidateRecurrence(pPattern) )
				{
					if ( pPattern.RecurrenceType == RecurrenceType.olRecursDaily )
					{
						aParam = new object[1];
						aParam[0] = ( pPattern.Interval == 1 ) ? "day" : string.Format("{0} days",pPattern.Interval);

						// Occurs every day effective 10/31/2006 from 8:00 AM to 8:30 AM.
						// Occurs every 50 days effective 10/31/2006 from 8:00 AM to 8:30 AM.
						// Occurs every weekday effective 10/31/2006 from 8:00 AM to 8:30 AM.
						Result = ( pPattern.Interval > 0 ) ? string.Format("every {0}",aParam) : "every weekday";
					}
					else if ( pPattern.RecurrenceType == RecurrenceType.olRecursWeekly )
					{
						aParam = new object[2];
						aParam[0] = ( pPattern.Interval == 1 ) ? "week" : string.Format("{0} weeks",pPattern.Interval);
						aParam[1] = this.GetDaysOfWeekText(pPattern.DayOfWeek);

						// Occurs every 3 weeks on Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, and Saturday effective 10/31/2006 from 8:00 AM to 8:30 AM.
						Result = string.Format("every {0} on {1}",aParam);
					}
					else if ( pPattern.RecurrenceType == RecurrenceType.olRecursMonthly )
					{
						aParam = new object[2];
						aParam[0] = pPattern.DayOfMonth;;
						aParam[1] = ( pPattern.Interval == 1 ) ? "1 month" : string.Format("{0} months",pPattern.Interval);

						// Occurs day 31 of every 1 month effective 10/31/2006 from 8:00 AM to 8:30 AM.
						Result = string.Format("day {0} of every {1}",aParam);
					}
					else if ( pPattern.RecurrenceType == RecurrenceType.olRecursMonthNth )
					{
						aParam = new object[3];
						aParam[0] = ((Instance)pPattern.Instance).ToString().ToLower();
						aParam[1] = this.GetDayOfWeekText(pPattern.DayOfWeek);
						aParam[2] = ( pPattern.Interval == 1 ) ? "1 month" : string.Format("{0} months",pPattern.Interval);

						// Occurs the third weekend day of every 32 months effective 6/13/2009 from 8:00 AM to 8:30 AM.					
						Result = string.Format("the {0} {1} of every {2}",aParam);
					}
					else if ( pPattern.RecurrenceType == RecurrenceType.olRecursYearly )
					{
						aParam = new object[2];
						aParam[0] = this.GetMonthName(pPattern.MonthOfYear);
						aParam[1] = pPattern.DayOfMonth;

						// Occurs every April 11 effective 4/11/2012 from 8:00 AM to 8:30 AM.
						Result = string.Format("every {0} {1}",aParam);
					}
					else if ( pPattern.RecurrenceType == RecurrenceType.olRecursYearNth )
					{
						aParam = new object[3];
						aParam[0] = ((Instance)pPattern.Instance).ToString().ToLower();
						aParam[1] = this.GetDayOfWeekText(pPattern.DayOfWeek);
						aParam[2] = this.GetMonthName(pPattern.MonthOfYear);

						// Occurs the third Sunday of February effective 2/17/2013 from 8:00 AM to 8:30 AM.
						Result = string.Format("the {0} {1} of {2}",aParam);
					}

					Result = string.Format("Occurs {0} effective {1:MM/dd/yyyy} from {2:HH:mm} to {3:HH:mm}",Result,pPattern.PatternStartDate,pPattern.StartTime,pPattern.EndTime);
				}

				return Result;
			}

			/// <summary>
			/// Validates a recurrence pattern and raises an exception if the pattern is not valid.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern to validate.</param>
			/// <returns>Whether the recurrence pattern is valid or not.</returns>
			public bool ValidateRecurrence(RecurrencePattern pPattern)
			{
				bool Valid = true;

				// On vérifie qu'il y a une date de fin
				// On vérifie les donnees utilisées selon le type de récurrence qui est utilisé

				if ( pPattern.Occurrences < 1 && pPattern.PatternEndDate == DateTime.MinValue )
				{
					this._RecurrencePatternException = new Exception("Recurrence has no end.");
					Valid = false;
				}
				else if ( pPattern.Occurrences < 1 && pPattern.PatternStartDate > pPattern.PatternEndDate)
				{
					this._RecurrencePatternException = new Exception("Start date must be lower than end date.");
					Valid = false;
				}
				else if ( pPattern.StartTime > pPattern.EndTime )
				{
					this._RecurrencePatternException = new Exception("Start time must be lower than end time.");
					Valid = false;
				}
				else if ( pPattern.RecurrenceType == RecurrenceType.olRecursDaily )
				{
					if ( pPattern.Interval == 0 && pPattern.DayOfWeek == 0 )
					{
						this._RecurrencePatternException = new Exception("Daily recurrence required an interval or to be set on weekday");
						Valid = false;
					}
				}
				else if ( pPattern.RecurrenceType == RecurrenceType.olRecursWeekly )
				{
					if ( pPattern.Interval < 1 || pPattern.DayOfWeek == 0)
					{
						this._RecurrencePatternException = new Exception("Weekly recurrence required an interval and day to occur on.");
						Valid = false;
					}
				}
				else if ( pPattern.RecurrenceType == RecurrenceType.olRecursMonthly )
				{
					if ( pPattern.Interval < 1 || pPattern.DayOfMonth < 1 )
					{
						this._RecurrencePatternException = new Exception("Monthly recurrence required an interval and day of the month.");
						Valid = false;
					}
				}
				else if ( pPattern.RecurrenceType == RecurrenceType.olRecursMonthNth )
				{
					if ( pPattern.Interval < 1 || (int)pPattern.Instance < 1 || (int)pPattern.DayOfWeek < 1 )
					{
						this._RecurrencePatternException = new Exception("Monthly recurrence required an interval, instance and day of the week.");
						Valid = false;
					}
				}
				else if ( pPattern.RecurrenceType == RecurrenceType.olRecursYearly )
				{
					if ( pPattern.Interval != 12 || pPattern.DayOfMonth < 1 || pPattern.MonthOfYear < 1 )
					{
						this._RecurrencePatternException = new Exception("Yearly recurrence required an interval, day of the month and month of the year.");
						Valid = false;
					}
				}
				else if ( pPattern.RecurrenceType == RecurrenceType.olRecursYearNth )
				{
					if ( pPattern.Interval != 12 || (int)pPattern.Instance < 1 || pPattern.MonthOfYear < 1 || (int)pPattern.DayOfWeek < 1)
					{
						this._RecurrencePatternException = new Exception("Yearly recurrence required an interval, instance, day of the week and month of the year.");
						Valid = false;
					}
				}
			
				// Lorsque tous les parametres sont valides, on mets l'exception a null
				if ( Valid )
					this._RecurrencePatternException = null;

				return Valid;
			}

			/// <summary>
			/// Gets specific month name
			///	Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pMonthIndex">Month index (1 to 12)</param>
			/// <returns>Month name</returns>
			public string GetMonthName(int pMonthIndex, CultureInfo poCultureInfo)
			{
				return poCultureInfo.DateTimeFormat.MonthNames[pMonthIndex-1];
			}

			/// <summary>
			/// Gets specific month name from default culture info (en-US).
			///	Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pMonthIndex">Month index (1 to 12)</param>
			/// <returns>Month name</returns>
			public string GetMonthName(int pMonthIndex)
			{
				return this.GetMonthName(pMonthIndex,new CultureInfo("en-US"));	
			}

			/// <summary>
			/// Gets month names based on specified culture info.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>DataSet</returns>
			public DataTable GetMonthNames(CultureInfo poCultureInfo)
			{			
				DataTable oDtMonth = new DataTable("Month");
				oDtMonth.Columns.Add("Id",typeof(int));
				oDtMonth.Columns.Add("Description",typeof(string));

				string Month;
				string[] aMonth = poCultureInfo.DateTimeFormat.MonthNames;
				for ( int MonthIndex = 0 ; MonthIndex < aMonth.Length ; MonthIndex++ )
				{	
					Month = aMonth[MonthIndex];
					if ( Month.Length > 0 )
						oDtMonth.Rows.Add(new object[] {MonthIndex + 1,Month});
				}

				return oDtMonth;
			}

			/// <summary>
			/// Gets month names from default culture info (en-US).
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <returns>DataSet</returns>
			public DataTable GetMonthNames()
			{
				return this.GetMonthNames(new CultureInfo("en-US"));
			}
		

			#endregion
			#region Private Method Member...

			/// <summary>
			/// Gets daily reccurrence
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern.</param>
			/// <returns>Collection of occurence determined from the recurrence pattern.</returns>
			private OccurrenceCollection GetDailyRecurrence(RecurrencePattern pPattern)
			{
				OccurrenceCollection oCollection = new OccurrenceCollection();

				// Date que nous allons utiliser pour creer les occurrences
				DateTime oDate = pPattern.PatternStartDate.Date;
				// Nombre maximum d'occurrence
				int MaxOccurence = this.GetMaxOccurrences(pPattern) - 1;
				// L'index courant de l'occurrence
				int OccurenceIndex = 0;
				// Quand ce flag est à false, on quitte la boucle car une des conditions d'arret à été atteinte		
				bool Continu = true;

				do
				{
					// On fait une validation afin de savoir si l'on doit créer une nouvelle occurrence.
					// Non  : On arrete
					// Oui  : On crée une nouvelle occurrence

					if( OccurenceIndex > MaxOccurence || oDate > pPattern.PatternEndDate )
						Continu = false;
					else
					{					
						oCollection.Add(new Occurrence(oDate.Add(pPattern.StartTime.TimeOfDay),oDate.Add(pPattern.EndTime.TimeOfDay)));

						if ( pPattern.Interval > 0 )
						{
							// Par interval
							// ----------------------
							// On ajoute le nombre de jour selon la valeur de l'interval
							oDate = oDate.AddDays(pPattern.Interval);			
						}
						else
						{
							// Weekday seulement
							// ----------------------
							// Lorsque c'est vendredi, on ajoute 3 jours pour tomber directement au lundi
							// sinon on ajoute qu'une journée pour avoir la journee suivante
							oDate = oDate.AddDays(( oDate.DayOfWeek == DayOfWeek.Friday ) ? 3 : 1);
						}

						OccurenceIndex++;
					}
				}
				while ( Continu );

				return oCollection;
			}

			/// <summary>
			/// Gets weekly reccurrence
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern.</param>
			/// <returns>Collection of occurence determined from the recurrence pattern.</returns>
			private OccurrenceCollection GetWeeklyRecurrence(RecurrencePattern pPattern)
			{
				OccurrenceCollection oCollection = new OccurrenceCollection();

				// Date que nous allons utiliser pour creer les occurrences
				DateTime oDate = pPattern.PatternStartDate.Date;
				// Representation de l'index du jour d'une date. Attention, DateTime.DayOfWeek commence a 0.
				int DayOfWeek;
				// Nombre maximum d'occurrence
				int MaxOccurence = this.GetMaxOccurrences(pPattern) - 1;
				// L'index courant de l'occurrence
				int OccurenceIndex = 0;
				// Quand ce flag est à false, on quitte la boucle car une des conditions d'arret à été atteinte		
				bool Continu = true;			

				do
				{
					// On fait une validation afin de savoir si l'on doit créer une nouvelle occurrence.
					// Non  : On arrete
					// Oui  : On crée une nouvelle occurrence

					if( OccurenceIndex > MaxOccurence || oDate > pPattern.PatternEndDate )
						Continu = false;
					else
					{			
						// On recupere le premier jours de la semaine et on va parcourir la semaine
						// entiere a la recherche des occurences à ajouter, selon les journées dans
						// la propriété RecurrencePattern.DayOfWeek
						oDate = oDate.AddDays(-1 * ((int)oDate.DayOfWeek + 1));
						for ( int DayOfWeekIndex = 0 ; DayOfWeekIndex < 7 ; DayOfWeekIndex++ )
						{
							// L'énumeration DateTime.DayOfWeek du Framework à pour valeur 0 à 6 et
							// commence au dimanche. Afin de pouvoir comparer le jour de la date et
							// le jour choisi dans RecurrencePattern.DayOfWeek, il faut faire une 
							// conversion afin de pouvoir utiliser une comparaison binaire (bitand).
							// Example : Dimanche : 2 ^ 0 = 1, Lundi : 2 ^ 1 = 2, Mardi = 2 ^ 2 = 4
							DayOfWeek = Convert.ToInt32(Math.Pow(2,(int)oDate.DayOfWeek));

							// On arrete la boucle si on depasse la date de fin du pattern en parcourant
							// les jours de la semaine et ainsi que si on a atteint le nombre d'occurrence
							// maximum.
							if ( oDate > pPattern.PatternEndDate || OccurenceIndex > MaxOccurence )
								break;
							else if ( oDate > pPattern.PatternStartDate && (DayOfWeek & (int)pPattern.DayOfWeek) == DayOfWeek )
							{
								// La date de l'occurrence est valide et
								// le jour fait parti du RecurrencePattern.DayOfWeek
								oCollection.Add(new Occurrence(oDate.Add(pPattern.StartTime.TimeOfDay),oDate.Add(pPattern.EndTime.TimeOfDay)));
								OccurenceIndex++;
							}

							oDate = oDate.AddDays(1);
						}					

						// Par interval
						// ----------------------
						// On ajoute le nombre de semaines : (7 jours * interval)
						oDate = oDate.AddDays(7 * pPattern.Interval);
					}
				}
				while ( Continu );

				return oCollection;
			}

			/// <summary>
			/// Gets monthly reccurrence
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern.</param>
			/// <returns>Collection of occurence determined from the recurrence pattern.</returns>
			private OccurrenceCollection GetMonthlyRecurrence(RecurrencePattern pPattern)
			{
				OccurrenceCollection oCollection = new OccurrenceCollection();

				// Date que nous allons utiliser pour creer les occurrences
				DateTime oDate = pPattern.PatternStartDate.Date;
				// Nombre maximum d'occurrence
				int MaxOccurence = this.GetMaxOccurrences(pPattern) - 1;
				// L'index courant de l'occurrence
				int OccurenceIndex = 0;
				// Quand ce flag est à false, on quitte la boucle car une des conditions d'arret à été atteinte		
				bool Continu = true;
	
				// Lorsque la date de départ est supérieure au RecurrencePattern.DayOfMonth, on ajuste
				// la date au 1er du prochain mois selon RecurrencePattern.Interval.
				if ( oDate.Day > pPattern.DayOfMonth )
					oDate = oDate.AddMonths(pPattern.Interval).AddDays(-1 * (oDate.Day - 1));

				do
				{
					// On fait une validation afin de savoir si l'on doit créer une nouvelle occurrence.
					// Non  : On arrete
					// Oui  : On crée une nouvelle occurrence

					if( OccurenceIndex > MaxOccurence || oDate > pPattern.PatternEndDate )
						Continu = false;
					else
					{	
						// Ajustement du jour (pour les jours du 29 au 31) :
						// - Lorsque le jour RecurrencePattern.DayOfMonth est supérieur au nombre de jours
						//   dans le mois, on ajuste la date au dernier jour du mois.
						// - Sinon, on ajoute le nombre de jours nécessaire
						if ( oDate.Day < pPattern.DayOfMonth )
							oDate = oDate.AddDays(((pPattern.DayOfMonth > DateTime.DaysInMonth(oDate.Year,oDate.Month)) ? DateTime.DaysInMonth(oDate.Year,oDate.Month) : pPattern.DayOfMonth) - oDate.Day);

						// Ajout à la collection
						oCollection.Add(new Occurrence(oDate.Add(pPattern.StartTime.TimeOfDay),oDate.Add(pPattern.EndTime.TimeOfDay)));

						// Par interval
						// ----------------------					
						// Ajoute le nombre de mois selon RecurrencePattern.Interval
						oDate = oDate.AddMonths(pPattern.Interval);

						OccurenceIndex++;
					}
				}
				while ( Continu );

				return oCollection;
			}

			/// <summary>
			/// Gets monthly day reccurrence
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern.</param>
			/// <returns>Collection of occurence determined from the recurrence pattern.</returns>
			private OccurrenceCollection GetMonthNthRecurrence(RecurrencePattern pPattern)
			{
				OccurrenceCollection oCollection = new OccurrenceCollection();

				// Utilisée pour creer les occurrences
				DateTime oDate = pPattern.PatternStartDate.Date;
				// Utilisée pour stocker la date d'Instance
				DateTime oDateInstance;
				// Nombre maximum d'occurrence
				int MaxOccurence = this.GetMaxOccurrences(pPattern) - 1;
				// L'index courant de l'occurrence
				int OccurenceIndex = 0;
				// Quand ce flag est à false, on quitte la boucle car une des conditions d'arret à été atteinte		
				bool Continu = true;
	
				// Lorsque la date de depart est plus grande que la date d'instance, on doit aller chercher la 
				// date de l'instance du prochain mois, sinon on utilise la date de l'instance
				oDateInstance = this.GetMonthNth(oDate,pPattern);
				oDate = ( oDate.Day < oDateInstance.Day ) ? oDateInstance : this.GetMonthNth(oDate.AddMonths(pPattern.Interval),pPattern);

				do
				{
					// On fait une validation afin de savoir si l'on doit créer une nouvelle occurrence.
					// Non  : On arrete
					// Oui  : On crée une nouvelle occurrence

					if( OccurenceIndex > MaxOccurence || oDate > pPattern.PatternEndDate )
						Continu = false;
					else
					{	
						// Ajout à la collection
						oCollection.Add(new Occurrence(oDate.Add(pPattern.StartTime.TimeOfDay),oDate.Add(pPattern.EndTime.TimeOfDay)));

						// Par interval
						// ----------------------					
						// Ajoute le nombre de mois selon RecurrencePattern.Interval
						oDate = this.GetMonthNth(oDate.AddMonths(pPattern.Interval),pPattern);

						OccurenceIndex++;
					}
				}
				while ( Continu );

				return oCollection;
			}		

			/// <summary>
			/// Gets yearly reccurrence
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern.</param>
			/// <returns>Collection of occurence determined from the recurrence pattern.</returns>
			private OccurrenceCollection GetYearlyRecurrence(RecurrencePattern pPattern)
			{
				OccurrenceCollection oCollection = new OccurrenceCollection();

				// Date que nous allons utiliser pour creer les occurrences
				DateTime oDate = pPattern.PatternStartDate.Date;
				// Date utilisée pour comparer la date du pattern avec la date de départ
				DateTime oDatePattern = new DateTime(oDate.Year,pPattern.MonthOfYear,(pPattern.DayOfMonth > DateTime.DaysInMonth(oDate.Year,pPattern.MonthOfYear)) ? DateTime.DaysInMonth(oDate.Year,pPattern.MonthOfYear) : pPattern.DayOfMonth);
				// Nombre maximum d'occurrence
				int MaxOccurence = this.GetMaxOccurrences(pPattern) - 1;
				// L'index courant de l'occurrence
				int OccurenceIndex = 0;
				// Quand ce flag est à false, on quitte la boucle car une des conditions d'arret à été atteinte		
				bool Continu = true;

				// Lorsque la date de départ est inférieure à celle du pattern, on change la date de départ pour le pattern
				// Lorsque la date de départ est supérieure au pattern, on ajoute 1 an au pattern pour avoir la prochaine occurence
				oDate = ( oDate < oDatePattern ) ? oDatePattern : oDatePattern.AddMonths(pPattern.Interval);

				do
				{
					// On fait une validation afin de savoir si l'on doit créer une nouvelle occurrence.
					// Non  : On arrete
					// Oui  : On crée une nouvelle occurrence

					if( OccurenceIndex > MaxOccurence || oDate > pPattern.PatternEndDate )
						Continu = false;
					else
					{	
						// Ajustement du jour (pour les jours du 29 au 31) :
						// - Lorsque le jour RecurrencePattern.DayOfMonth est supérieur au nombre de jours
						//   dans le mois, on ajuste la date au dernier jour du mois.
						// - Sinon, on ajoute le nombre de jours nécessaire
						if ( oDate.Day < pPattern.DayOfMonth )
							oDate = oDate.AddDays(((pPattern.DayOfMonth > DateTime.DaysInMonth(oDate.Year,oDate.Month)) ? DateTime.DaysInMonth(oDate.Year,oDate.Month) : pPattern.DayOfMonth) - oDate.Day);

						// Ajout à la collection
						oCollection.Add(new Occurrence(oDate.Add(pPattern.StartTime.TimeOfDay),oDate.Add(pPattern.EndTime.TimeOfDay)));

						// Par interval
						// ----------------------					
						// Ajoute 12 mois
						oDate = oDate.AddMonths(pPattern.Interval);

						OccurenceIndex++;
					}
				}
				while ( Continu );

				return oCollection;
			}

			/// <summary>
			/// Gets yearly reccurrence
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pPattern">Recurrence pattern.</param>
			/// <returns>Collection of occurence determined from the recurrence pattern.</returns>
			private OccurrenceCollection GetYearNthRecurrence(RecurrencePattern pPattern)
			{
				OccurrenceCollection oCollection = new OccurrenceCollection();

				// Date que nous allons utiliser pour creer les occurrences
				DateTime oDate = pPattern.PatternStartDate.Date;
				// Date utilisée pour comparer la date du pattern avec la date de départ
				DateTime oDatePattern = this.GetMonthNth(new DateTime(oDate.Year,pPattern.MonthOfYear,1),pPattern);
				// Nombre maximum d'occurrence
				int MaxOccurence = this.GetMaxOccurrences(pPattern) - 1;
				// L'index courant de l'occurrence
				int OccurenceIndex = 0;
				// Quand ce flag est à false, on quitte la boucle car une des conditions d'arret à été atteinte		
				bool Continu = true;

				// Lorsque la date de départ est inférieure à celle du pattern, on change la date de départ pour le pattern
				// Lorsque la date de départ est supérieure au pattern, on ajoute 1 an au pattern pour avoir la prochaine occurence
				oDate = ( oDate < oDatePattern ) ? oDatePattern : oDatePattern.AddMonths(pPattern.Interval);

				do
				{
					// On fait une validation afin de savoir si l'on doit créer une nouvelle occurrence.
					// Non  : On arrete
					// Oui  : On crée une nouvelle occurrence

					if( OccurenceIndex > MaxOccurence || oDate > pPattern.PatternEndDate )
						Continu = false;
					else
					{	
						// Ajout à la collection
						oCollection.Add(new Occurrence(oDate.Add(pPattern.StartTime.TimeOfDay),oDate.Add(pPattern.EndTime.TimeOfDay)));

						// Par interval
						// ----------------------					
						// Ajoute le nombre de mois selon RecurrencePattern.Interval
						oDate = this.GetMonthNth(oDate.AddMonths(pPattern.Interval),pPattern);

						OccurenceIndex++;
					}
				}
				while ( Continu );

				return oCollection;
			}		

			/// <summary>
			/// Gets datetime of a Nth day of a month.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pDate">DateTime (year and month are used).</param>
			/// <param name="pPattern">Recurrence pattern</param>
			/// <returns>Nth day of a month</returns>
			private DateTime GetMonthNth(DateTime pDate,RecurrencePattern pPattern)
			{
				// Le jour du mois a partir duquel on va creer une date lors du return;			
				int Day = 1;

				int Instance = (int)pPattern.Instance;

				// Instance :
				// First 1
				// Second 2
				// Third 3
				// Fourth 4
				// Last 5

				if ( pPattern.DayOfWeek == DaysOfWeek.Day )
				{
					// Instance 1 a 4 : Instance
					// Instance 5	  : Dernier jour du mois
					Day = ( Instance < 5 ) ? Instance : DateTime.DaysInMonth(pDate.Year,pDate.Month);
				}
				else if ((Instance < 5 && (pPattern.DayOfWeek == DaysOfWeek.Weekday || pPattern.DayOfWeek == DaysOfWeek.WeekendDay)) || Instance == 5 )
				{				
					// Instance 1 a 4 avec Weekday ou WeekendDay
					// Instance 5 excluant Day

					int DayOfWeek;   // Index du jour de la semaine
					DateTime oDate;   // Date utilisée pour aller chercher le Nieme jour
				
					if ( Instance < 5 )
					{// Instance 1 a 4 avec Weekday ou WeekendDay

						// Date de depart, on ajoute une journee a chaque itérations;
						oDate = new DateTime(pDate.Year,pDate.Month,1);
					
						int InstanceIndex = 0;
						while ( InstanceIndex < Instance )
						{
							DayOfWeek = (int)oDate.DayOfWeek;
							if ((pPattern.DayOfWeek == DaysOfWeek.Weekday && 0 < DayOfWeek && DayOfWeek < 6) ||
								(pPattern.DayOfWeek == DaysOfWeek.WeekendDay && (DayOfWeek == 0 || DayOfWeek == 6 )))
							{
								InstanceIndex++;

								// On ne veut pas faire une boucle pour rien donc on sort du while
								if ( InstanceIndex == Instance )
									break;
							}
						
							oDate = oDate.AddDays(1);
						}
					}
					else
					{// Instance 5 excluant Day

						// Date de depart, on soustrait une journee a chaque itérations;
						oDate = new DateTime(pDate.Year,pDate.Month,DateTime.DaysInMonth(pDate.Year,pDate.Month));
						while (true)
						{
							DayOfWeek = (int)oDate.DayOfWeek;
							if ((pPattern.DayOfWeek == DaysOfWeek.Weekday && 0 < DayOfWeek && DayOfWeek < 6) ||
								(pPattern.DayOfWeek == DaysOfWeek.WeekendDay && (DayOfWeek == 0 || DayOfWeek == 6 )) ||
								(pPattern.DayOfWeek == DaysOfWeek.Sunday	&& DayOfWeek == 0 ) ||
								(pPattern.DayOfWeek == DaysOfWeek.Monday	&& DayOfWeek == 1 ) ||
								(pPattern.DayOfWeek == DaysOfWeek.Tuesday	&& DayOfWeek == 2 ) ||
								(pPattern.DayOfWeek == DaysOfWeek.Wednesday && DayOfWeek == 3 ) ||
								(pPattern.DayOfWeek == DaysOfWeek.Thursday	&& DayOfWeek == 4 ) ||
								(pPattern.DayOfWeek == DaysOfWeek.Friday	&& DayOfWeek == 5 ) ||
								(pPattern.DayOfWeek == DaysOfWeek.Saturday	&& DayOfWeek == 6 )
								)
							{
								break;
							}
							else
								oDate = oDate.AddDays(-1);
						}
					}
				
					// On recupere la bonne journee du mois
					Day = oDate.Day;
				}
				else
				{
					// On recupere le weekday du premier du mois sous format (1 a 7)
					int DayOfWeekDay1 = ((int)new DateTime(pDate.Year,pDate.Month,1).DayOfWeek) + 1;
					// On recupere le weekday du pattern sous format (1 a 7)
					int DayOfWeekPattern = (int)Math.Round(Math.Log((int)pPattern.DayOfWeek,2),2) + 1;
				
					// Initialise de tel maniere que lorsque DayOfWeekDay1 et DayOfWeekPattern sont les memes, il n'y a
					// aucun jour à ajouter
					int DaysToAdd = 0;

					// On se déplace de quelques jours pour pointer sur la bonne journée
					if ( DayOfWeekDay1 > DayOfWeekPattern )
						DaysToAdd = 7 - (DayOfWeekDay1 - DayOfWeekPattern);
					else if ( DayOfWeekDay1 < DayOfWeekPattern )
						DaysToAdd = DayOfWeekPattern - DayOfWeekDay1;

					// day 1 + jour ajoutés + instance
					Day = 1 + DaysToAdd + (7 * (Instance - 1));
				}

				return new DateTime(pDate.Year,pDate.Month,Day);
			}

			/// <summary>
			/// Gets concatenation of all week days contained in bitand parameter.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pDayOfWeek">Days of week</param>
			/// <returns>String with selected days</returns>
			public string GetDaysOfWeekText(DaysOfWeek pDayOfWeek)
			{
				int DayOfWeek = (int)pDayOfWeek;
				StringBuilder oSb = new StringBuilder("");
				string Result = "";

				if ( ((int)DaysOfWeek.Sunday & DayOfWeek) == (int)DaysOfWeek.Sunday )
					oSb.AppendFormat((oSb.Length == 0) ? "{0}" : ", {0}",DaysOfWeek.Sunday);
				if ( ((int)DaysOfWeek.Monday & DayOfWeek) == (int)DaysOfWeek.Monday )
					oSb.AppendFormat((oSb.Length == 0) ? "{0}" : ", {0}",DaysOfWeek.Monday);
				if ( ((int)DaysOfWeek.Tuesday & DayOfWeek) == (int)DaysOfWeek.Tuesday )
					oSb.AppendFormat((oSb.Length == 0) ? "{0}" : ", {0}",DaysOfWeek.Tuesday);
				if ( ((int)DaysOfWeek.Wednesday & DayOfWeek) == (int)DaysOfWeek.Wednesday )
					oSb.AppendFormat((oSb.Length == 0) ? "{0}" : ", {0}",DaysOfWeek.Wednesday);
				if ( ((int)DaysOfWeek.Thursday & DayOfWeek) == (int)DaysOfWeek.Thursday )
					oSb.AppendFormat((oSb.Length == 0) ? "{0}" : ", {0}",DaysOfWeek.Thursday);
				if ( ((int)DaysOfWeek.Friday & DayOfWeek) == (int)DaysOfWeek.Friday )
					oSb.AppendFormat((oSb.Length == 0) ? "{0}" : ", {0}",DaysOfWeek.Friday);
				if ( ((int)DaysOfWeek.Saturday & DayOfWeek) == (int)DaysOfWeek.Saturday )
					oSb.AppendFormat((oSb.Length == 0) ? "{0}" : ", {0}",DaysOfWeek.Saturday);

				Result = oSb.ToString();

				// Wednesday has 9 letters so when there is more than one day, we have to replace
				// the last coma with " and"
				if ( Result.Length > 10 ) 
				{				
					int IndexOf = Result.LastIndexOf(",");
					if ( IndexOf > 0 )
						Result = Result.Remove(IndexOf,1).Insert(IndexOf," and");
				}
				return Result;
			}

			/// <summary>
			/// Gets exact day of the week text.
			/// Author : Pascal Groulx (2006/11/03)
			/// </summary>
			/// <param name="pDayOfWeek">Day of the week</param>
			/// <returns>String with selected day</returns>
			public string GetDayOfWeekText(DaysOfWeek pDayOfWeek)
			{
				string Result = "";

				if ( pDayOfWeek == DaysOfWeek.Day || pDayOfWeek == DaysOfWeek.Weekday )
				{
					// On doit retourner ses day of week en lower case
					Result = pDayOfWeek.ToString().ToLower();
				}
				else if ( pDayOfWeek == DaysOfWeek.WeekendDay )
				{
					// On doit ajouter un espace dans WeekendDay et en lower case
					Result = pDayOfWeek.ToString().Insert(7," ").ToLower();
				}
				else
					Result = pDayOfWeek.ToString();

				return Result;
			}
			/// <summary>
			/// Gets maximum number of occurrences allowed for the recurrence.
			/// Author : Pascal Groulx (2006/11/14))
			/// </summary>
			/// <param name="pPattern">Recurrence pattern.</param>
			/// <returns>int</returns>
			private int GetMaxOccurrences(RecurrencePattern pPattern)
			{
				// Lorsque RecurrencePattern.Occurrences = 0 : c'est RecurrencePattern.PatternEndDate qui précise la fin
				//	Par contre, on doit quand même imposer une limite d'occurrences
				// La même validation est imposé à un nombre d'occurrence plus grand que le nombre maximum permis
				return ( pPattern.Occurrences == 0 || pPattern.Occurrences > this._MaxOccurrence ) ? this._MaxOccurrence : pPattern.Occurrences;
			}


			#endregion

			#region Public Class Member
			#endregion
			#region Private Class Member
			#endregion		
		}


		/// <summary>
		/// Collection of occurence determined by a recurrence pattern.
		/// Author : Pascal Groulx (2006/11/03)
		/// </summary>
		public class OccurrenceCollection : ICollection
		{
			#region Public Property Member
			public Occurrence LastOccurence
			{
				get
				{
					if (this._Collection != null && this._Collection.Count > 0)
					{
						return (Occurrence)this._Collection[this._Collection.Count - 1];
					}
					else
					{
						return null;
					}
				}
			}
			#endregion
			#region Private Property Member...

			/// <summary>Collection of occurrence</summary>
			private ArrayList _Collection;

			#endregion

			#region Public Method Member...
			#region Contructor...

			/// <summary>
			/// Constructor
			/// </summary>
			public OccurrenceCollection()
			{
				this._Collection = new ArrayList();
			}

			#endregion

			/// <summary>
			/// Add an occurence into the collection.
			/// </summary>
			/// <param name="pOccurrence">The occurrence to add to the collection.</param>
			public void Add(Occurrence pOccurrence)
			{
				this._Collection.Add(pOccurrence);
			}
			#endregion
			#region Private Method Member
			#endregion

			#region Public Class Member
			#endregion
			#region Private Class Member
			#endregion	

			#region ICollection Members...

			/// <summary>Gets a value indicating whether access to the System.Collections.ArrayList is synchronized (thread-safe).</summary>
			public bool IsSynchronized{get{return this._Collection.IsSynchronized;}}

			/// <summary>Gets the number of elements actually contained in the System.Collections.ArrayList.</summary>
			public int Count{get{return this._Collection.Count;}}

			/// <summary>Copies a range of elements from the System.Collections.ArrayList to a compatible one-dimensional System.Array, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional System.Array that is the destination of the elements copied from System.Collections.ArrayList. The System.Array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the source System.Collections.ArrayList at which copying begins.</param>
			public void CopyTo(Array array, int index)
			{
				this._Collection.CopyTo(array,index);
			}

			/// <summary>Gets an object that can be used to synchronize access to the System.Collections.ArrayList.</summary>
			public object SyncRoot {get{return this._Collection.SyncRoot;}}

			#endregion
			#region IEnumerable Members...

			/// <summary>
			/// Returns an enumerator for a section of the System.Collections.ArrayList.  
			/// </summary>
			/// <returns>An System.Collections.IEnumerator for the specified section of the System.Collections.ArrayList.</returns>
			public System.Collections.IEnumerator GetEnumerator()
			{
				return this._Collection.GetEnumerator();
			}

			#endregion
		}

		/// <summary>
		/// Contains a date range (start and end).
		/// Author : Pascal Groulx (2006/11/03)
		/// </summary>
		public class DateRange
		{
			#region Public Property Member...

			/// <summary>Gets/sets range start date.</summary>
			public DateTime StartDate {get{return this._StartDate;}set{this._EndDate = value;}}
			/// <summary>Gets/sets range end date.</summary>
			public DateTime EndDate {get{return this._EndDate;}set{this._EndDate = value;}}

			#endregion
			#region Private Property Member...

			/// <summary>Occurence start date.</summary>
			private DateTime _StartDate;
			/// <summary>Occurence end date.</summary>
			private DateTime _EndDate;

			#endregion

			#region Public Method Member...
			#region Contructor...

	
			/// <summary>
			/// Constructor
			/// </summary>
			public DateRange(){}

			/// <summary>
			/// Constructor overload
			/// </summary>
			/// <param name="pStartDate">Start date</param>
			/// <param name="pEndDate">End date</param>
			public DateRange(DateTime pStartDate,DateTime pEndDate) : this()
			{
				this._StartDate = pStartDate;
				this._EndDate = pEndDate;
			}

			#endregion
			#endregion
			#region Private Method Member
			#endregion

			#region Public Class Member
			#endregion
			#region Private Class Member
			#endregion		
		}

		/// <summary>
		/// An occurence is created from recurrence pattern.
		/// Author : Pascal Groulx (2006/11/03)
		/// </summary>
		public class Occurrence : DateRange
		{
			#region Public Property Member
			#endregion
			#region Private Property Member
			#endregion

			#region Public Method Member
			#region Contructor
	
			/// <summary>
			/// Constructor
			/// </summary>
			public Occurrence() : base() {}

			/// <summary>
			/// Constructor overload
			/// </summary>
			/// <param name="pStartDate">Start date</param>
			/// <param name="pEndDate">End date</param>
			public Occurrence(DateTime pStartDate,DateTime pEndDate) : base(pStartDate,pEndDate) {}

			#endregion
			#endregion
			#region Private Method Member
			#endregion

			#region Public Class Member
			#endregion
			#region Private Class Member
			#endregion		
		}

		/// <summary>
		/// Recurrence pattern class.
		/// Author : Pascal Groulx (2006/11/03)
		/// </summary>
		public class RecurrencePattern
		{
			#region Public Property Member...

			/// <summary>Gets/sets the activity start time, only hours and minutes of this date are used.</summary>
			public DateTime StartTime{get{return this._StartTime;}set{this._StartTime = value;}}
			/// <summary>Gets/sets the activity end time, only hours and minutes of this date are used.</summary>
			public DateTime EndTime{get{return this._EndTime;}set{this._EndTime = value;}}

			/// <summary>Gets/sets the first date that the recurrence occurs. You can shift around the start time; It doesn't seem to need to be the date of the initial calendar.</summary>
			public DateTime PatternStartDate{get{return this._PatternStartDate;}set{this._PatternStartDate = value;}}
			/// <summary>Gets/sets the last date that the recurrence occurs.</summary>
			public DateTime PatternEndDate{get{return this._PatternEndDate;}set{this._PatternEndDate = value;}}

			/// <summary>Gets/sets the recurrence type.</summary>
			public RecurrenceType RecurrenceType{get{return this._RecurrenceType;}set{this._RecurrenceType = value;}}

			/// <summary>Gets/sets the number of days, months or years between occurrances. If it's 1, that means that there's an occurance every day, month, or year.</summary>
			public int Interval{get{return this._Interval;}set{this._Interval = value;}}
			/// <summary>Gets/sets which instance to used. It determines the first (1), second (2), third (3), fourth (4) or last (5) day in a month.</summary>
			public Instance Instance{get{return this._Instance;}set{this._Instance = value;}}

			/// <summary>Gets/sets the number of times that the occurrence happens.</summary>
			public int Occurrences{get{return this._Occurrences;}set{this._Occurrences = value;}}

			/// <summary>Gets/sets the day of month on which the recurrence must occurs.</summary>
			public int DayOfMonth{get{return this._DayOfMonth;}set{this._DayOfMonth = value;}}
			/// <summary>Gets/sets days of week on which the recurrence must occurs.</summary>
			public DaysOfWeek DayOfWeek{get{return this._DayOfWeek;}set{this._DayOfWeek = value;}}
			/// <summary>Gets/sets month of the year on which the recurrence must occurs.</summary>
			public int MonthOfYear{get{return this._MonthOfYear;}set{this._MonthOfYear = value;}}

			#endregion
			#region Private Property Member...

			/// <summary>Day of month on which the recurrence must occurs.</summary>
			private int _DayOfMonth = 1;
			/// <summary>Days of week on which the recurrence must occurs.</summary>
			private DaysOfWeek _DayOfWeek = 0;
			/// <summary>Month of the year on which the recurrence must occurs.</summary>
			private int _MonthOfYear = 1;
			/// <summary>This is the number of (days, months, years) between occurrances. If it's 1, that means that there's an occurance every day, month, or year.</summary>
			private int _Interval = 1;
			/// <summary>Determines first (1), second (2), third (3), fourth (4) or last (5) day in a month.</summary>
			private Instance _Instance = Instance.First;
			/// <summary>
			/// This is the number of times that the occurrence happens. If it's 13, and you're operating daily 
			/// (RecurrenceType property set to 0, olRecursDaily) with the Interval property set to 2, that means
			/// it'll happen every other day, a total of 13 times. That means you're stretching 2*13=26 days into
			/// the future.
			/// </summary>
			private int _Occurrences = 0;
			/// <summary>This is the recurrence type, activity can recurs daily, weekly etc.</summary>
			private RecurrenceType _RecurrenceType = RecurrenceType.olRecursDaily;
			/// <summary>
			/// This is the first date that the recurrence occurs. Opposite RecurrencePatternPatternEndDateProperty.
			///	You can shift around the start time; It doesn't seem to need to be the date of the initial calendar.
			/// </summary>
			private DateTime _PatternStartDate = DateTime.Now.Date;
			/// <summary>This is the last date that the recurrence occurs. Opposite PatternStartDate property.</summary>
			private DateTime _PatternEndDate = DateTime.MinValue;
			/// <summary>Activity start time, only hour and minute of this date are used.</summary>
			private DateTime _StartTime = DateTime.Now;
			/// <summary>Activity end time, only hour and minute of this date are used.</summary>
			private DateTime _EndTime = DateTime.Now;

			#endregion

			#region Public Method Member
			#region Contructor
			#endregion
			#endregion
			#region Private Method Member
			#endregion

			#region Public Class Member
			#endregion
			#region Private Class Member
			#endregion	
		}
	}
}
