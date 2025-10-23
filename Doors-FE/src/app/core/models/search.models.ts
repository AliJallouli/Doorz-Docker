export interface JobResult {
  id: number; // job_offer_id
  title: string; // Titre traduit (job_offer_translations.title)
  company: string; // Nom de l'entreprise (company.name)
  location: string; // Lieu (job_offer.location)
  offerType: string; // Type d'offre traduit (offer_type_translations.translated_name)
  contractType: string; // Type de contrat traduit (contract_type_translations.translated_name)
  scheduleType: string; // Type d'horaire traduit (schedule_type_translations.translated_name)
  salary: number; // Salaire horaire (job_offer.salary)
  currency: string; // Code de la devise (currency.code, ex. "EUR")
  startDate: string; // Date de début au format ISO (job_offer.start_date)
  isActive: boolean; // Statut actif (job_offer.is_active)
  description?: string; // Description traduite (job_offer_translations.description, optionnel)
  language: string; // Code de la langue (spoken_language.code, ex. "fr")
}

export interface InternshipResult {
  id: number; // offer_id from offer table
  title: string; // Translated title (offer_translations.translated_title)
  company: string; // Company name (company.name, possibly from company_translations.translated_name)
  location: string; // Location (concatenation of location.city, location.country, or location.street)
  offerType: string; // Translated offer type (offer_type_translations.translated_name, e.g., "Stage")
  contractType: string; // Translated contract type (contract_type_translations.translated_name, e.g., "CDD")
  scheduleType: string; // Translated schedule type (schedule_type_translations.translated_name, e.g., "Plein temps")
  salary?: number; // Hourly salary (offer.salary, optional as not always specified)
  currency?: string; // Currency code (currency.code, e.g., "EUR", optional)
  startDate: string; // Start date in ISO format (offer.start_date, e.g., "2025-06-01")
  isActive: boolean; // Active status (offer.active)
  description?: string; // Translated description (offer_translations.translated_description, optional)
  language: string; // Language code (spoken_language.code, e.g., "fr")
  duration?: number; // Duration of the internship (offer.duration, optional)
  durationUnit?: string; // Translated duration unit (duration_unit_translations.translated_name, e.g., "mois", optional)
  remotePossible?: boolean; // Remote work possibility (offer.remote_possible, optional)
  deadline?: string; // Application deadline in ISO format (offer.deadline, optional)
}

export interface StudyResult {
  id: number; // degree_id from degree table
  title: string; // Translated degree name (degree_translations.translated_name)
  institution: string; // Institution name (institution.name, possibly from institution_translations.translated_name)
  location: string; // Location (concatenation of location.city, location.country, or location.street)
  degreeType: string; // Translated degree type (degree_type_translations.translated_name, e.g., "Bachelier")
  category?: string; // Translated degree category (degree_category_translations.translated_name, optional)
  duration?: number; // Duration of the program (degree.duration, optional)
  durationUnit?: string; // Translated duration unit (duration_unit_translations.translated_name, e.g., "années", optional)
  credits?: number; // ECTS credits (degree.credits, optional)
  cost?: number; // Cost of the program (degree.cost, optional)
  currency?: string; // Currency code (currency.code, e.g., "EUR", optional)
  language: string; // Language code (spoken_language.code, e.g., "fr")
  deliveryMode?: string; // Translated delivery mode (delivery_mode_translations.translated_name, e.g., "Présentiel", optional)
  isActive: boolean; // Active status (degree.is_active)
  description?: string; // Translated description (degree_translations.translated_description, optional)
}
export interface AidResult {
  id: number; // aid_id (hypothetical aid table or derived from payment_plan.payment_plan_id)
  title: string; // Translated title (aid_translations.translated_name or payment_plan_translations.translated_name)
  description: string; // Translated description (aid_translations.translated_description or payment_plan_translations.translated_description)
  location: string; // Location (concatenation of location.city, location.country, or institution location)
  provider?: string; // Provider name (e.g., institution.name or external entity, optional)
  amount?: number; // Aid amount (e.g., derived from payment_plan.price or aid-specific field, optional)
  currency?: string; // Currency code (currency.code, e.g., "EUR", optional)
  applicationDeadline?: string; // Application deadline in ISO format (hypothetical aid.deadline, optional)
  isActive?: boolean; // Active status (e.g., based on deadline or payment_plan availability, optional)
  language: string; // Language code (spoken_language.code, e.g., "fr")
}

export interface EventResult {
  id: number; // event_id from event table
  title: string; // Translated title (event_translations.translated_title)
  organizer: string; // Organizer name (entities.name or entity-specific name from institution/company, possibly translated)
  location: string; // Location (concatenation of location.city, location.country, or location.street)
  startDate: string; // Start date and time in ISO format (event.start_date_time, e.g., "2025-06-01T10:00:00Z")
  endDate?: string; // End date and time in ISO format (event.end_date_time, optional)
  description?: string; // Translated description (event_translations.translated_description, optional)
  isActive: boolean; // Active status (derived from event dates or explicit status)
  language: string; // Language code (spoken_language.code, e.g., "fr")
  registrationRequired?: boolean; // Registration requirement (event.registration_required, optional)
  registrationLink?: string; // Registration link (event.registration_link, optional)
  isPublic?: boolean; // Public event status (event.is_public, optional)
}

export interface KotResult {
  id: number; // aid_id (hypothetical aid table or derived from payment_plan.payment_plan_id)
  title: string; // Translated title (aid_translations.translated_name or payment_plan_translations.translated_name)
  description: string; // Translated description (aid_translations.translated_description or payment_plan_translations.translated_description)
  location: string; // Location (concatenation of location.city, location.country, or institution location)
  provider?: string; // Provider name (e.g., institution.name or external entity, optional)
  amount?: number; // Aid amount (e.g., derived from payment_plan.price or aid-specific field, optional)
  currency?: string; // Currency code (currency.code, e.g., "EUR", optional)
  applicationDeadline?: string; // Application deadline in ISO format (hypothetical aid.deadline, optional)
  isActive?: boolean; // Active status (e.g., based on deadline or payment_plan availability, optional)
  language: string; // Language code (spoken_language.code, e.g., "fr")
}
