#include <stdio.h>
#include <stdbool.h>

// A Union is a composite type where only one field can be set.

// A Contact can either be an email string, a phone extension (4 digits),
// OR a 10 digit phone number.
union Contact {
	char* email;
	short extension;
	long long phoneNumber;
};

struct TaggedContact {
	union Contact theContact;
	enum ContactType {Email, Ext, Phone} contactType;
};

int main() {
	printf("Contact union is %d bytes\n", sizeof(union Contact));
	// The union is the same size as the largest of its fields, regardless of which field is actually used.

	union Contact neal;
	neal.email = "neal.terrell@csulb.edu";
	struct TaggedContact n;
	n.theContact = neal;
	n.contactType = Email;

	
	printf("neal is at \t\t%p\n", &neal);
	printf("extension is at \t%p\twith value %hd\n", &neal.extension, neal.extension);
	printf("phoneNumber is at \t%p\twith value %lld\n", &neal.phoneNumber, neal.phoneNumber);
	printf("email is at \t\t%p\twith value %s\n\n", &neal.email, neal.email);


	union Contact anthony;
	anthony.phoneNumber = 9876543210;
	printf("anthony is at \t\t%p\n", &anthony);
	printf("extension is at \t%p\twith value %hd\n", &anthony.extension, anthony.extension);
	printf("phoneNumber is at \t%p\twith value %lld\n", &anthony.phoneNumber, anthony.phoneNumber);
	printf("email is at \t\t%p\twith value %s\n", &anthony.email, anthony.email);
}