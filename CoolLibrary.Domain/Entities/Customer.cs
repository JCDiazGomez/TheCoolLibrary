using CoolLibrary.Domain.Enums;

namespace CoolLibrary.Domain.Entities;

/// <summary>
/// Represents a library customer/member
/// </summary>
public class Customer
{
    /// <summary>
    /// Unique identifier for the customer
    /// </summary>
    public int CustomerId { get; set; }
    
    /// <summary>
    /// Customer's first name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Customer's last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Customer's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Customer's phone number
    /// </summary>
    public string? Phone { get; set; }
    
    /// <summary>
    /// Customer's street address
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Customer's city
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Customer's postal code
    /// </summary>
    public string? PostalCode { get; set; }
    
    /// <summary>
    /// Date when customer became a library member
    /// </summary>
    public DateTime MembershipDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Current membership status
    /// </summary>
    public MembershipStatus MembershipStatus { get; set; } = MembershipStatus.Active;
    
    /// <summary>
    /// Maximum number of books this customer can borrow simultaneously
    /// </summary>
    public int MaxBooksAllowed { get; set; } = 5;
    
    /// <summary>
    /// Date when the customer record was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Date when the customer record was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    
    /// <summary>
    /// One-to-many relationship with Loans
    /// </summary>
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    
    /// <summary>
    /// One-to-many relationship with Reservations
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    
    /// <summary>
    /// One-to-many relationship with Fines
    /// </summary>
    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();
    
    /// <summary>
    /// Full name property for convenience
    /// </summary>
    public string FullName => $"{FirstName} {LastName}".Trim();
    
    /// <summary>
    /// Current number of active loans
    /// </summary>
    public int CurrentLoanCount => Loans?.Count(l => l.Status == LoanStatus.Active || l.Status == LoanStatus.Overdue) ?? 0;
    
    /// <summary>
    /// Indicates if customer can borrow more books
    /// </summary>
    public bool CanBorrowMoreBooks => CurrentLoanCount < MaxBooksAllowed && MembershipStatus == MembershipStatus.Active;
}