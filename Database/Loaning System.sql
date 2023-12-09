CREATE TABLE user_account(user_id SERIAL NOT NULL PRIMARY KEY, user_username VARCHAR(50) NOT NULL, user_password VARCHAR(50) NOT NULL, first_name VARCHAR(50) NOT NULL, last_name VARCHAR(50) NOT NULL, address VARCHAR(50) NOT NULL, phone_number VARCHAR(50) NOT NULL);

CREATE TABLE creditors(creditor_id SERIAL NOT NULL PRIMARY KEY, company_name VARCHAR(100) NOT NULL, loan_type VARCHAR(100) NOT NULL, interest DECIMAL(10,2));

CREATE TABLE debt (debt_id SERIAL NOT NULL PRIMARY KEY, user_id BIGINT UNSIGNED, FOREIGN KEY(user_id) REFERENCES user_account(user_id),
                  creditor_id BIGINT UNSIGNED, FOREIGN KEY(creditor_id) REFERENCES creditors(creditor_id), amount INT NOT NULL, monthly_payment DECIMAL(10,2) NOT NULL,
                  debt_date DATE, due_date DATE, is_active BOOLEAN);

CREATE TABLE payment(pay_id SERIAL NOT NULL PRIMARY KEY, debt_id BIGINT UNSIGNED NOT NULL,
                     FOREIGN KEY(debt_id) REFERENCES debt(debt_id), value DECIMAL (10,2) NOT NULL,
                     method VARCHAR(50) NOT NULL, payment_date DATE NOT NULL, user_id BIGINT UNSIGNED NOT NULL, FOREIGN KEY(user_id) REFERENCES user_account(user_id));

insert into user_account (user_username, user_password, first_name, last_name, address, phone_number) VALUES
('arthur_morgan', 'password', 'Arthur', 'Morgan', 'Dewberry Creek', '123-456-7890'),
('sadie_adler', 'password123', 'Sadie', 'Adler', 'Copperhead Landing', '098-765-4321'),
('john_marston', 'password456', 'John', 'Marston', 'Roanoke Ridge', '123-579-0310'),
('hosea_matthews', 'password789', 'Hosea', 'Matthews', 'New Hanover', '987-654-3210'),
('micah_bell', 'password890', 'Micah', 'Bell', 'Butcher Creek', '555-123-4567'),
('javier_escuella', 'password098', 'Javier', 'Escuella', 'Scarlett Meadows', '777-888-9999'),
('charles_smith', 'password987', 'Charles', 'Smith', 'Cumberland Forest', '222-333-4444'),
('abigail_roberts', 'password654', 'Abigail', 'Roberts', 'Bluewater Marsh', '999-000-1111'),
('bill_williamson', 'password321', 'Bill', 'Williamson', 'Cholla Springs', '123-555-7777'),
('josiah_trelawny', 'password246', 'Josiah', 'Trelawny', 'Rio Bravo', '333-222-1111');

insert into creditors(company_name, loan_type, interest) VALUES
('Smithfields Garage', 'Car Loan', 0.02),
('BullWorth Academy', 'Student Loan', 0.03),
('Saint Denis Appliances', 'Appliance Loan', 0.04),
('Annesburg Housing', 'Housing Loan', 0.05),
('Valentine Savings Bank', 'Personal Loan', 0.04),
('Saint Jacob Health Services', 'Hospital Loan', 0.03),
('Cornwall Enterprise', 'Business Loan', 0.02),
('Tacitus Travel Agency', 'Travel Loan', 0.04),
('Kilgore Autocare', 'Car Loan', 0.03),
('New Austin Bank', 'Personal Loan', 0.05);

insert into debt(user_id, creditor_id, amount, monthly_payment, debt_date, due_date, is_active) VALUES
(1, 1, 103055.75, 2944.45, '2023-11-09', '2026-11-09', true),
(2, 2, 113083.53, 1916.67, '2023-11-09', '2028-11-09', true),
(3, 3, 108888.85, 3111.11, '2023-11-09', '2026-11-09', true),
(4, 4, 122917.06, 2083.34, '2023-11-09', '2028-11-09', true),
(5, 5, 118000.00, 2000.00, '2023-11-09', '2028-11-09', true),
(6, 6, 105972.30, 3027.78, '2023-11-09', '2026-11-09', true),
(7, 7, 108167.06, 1833.34, '2023-11-09', '2028-11-09', true),
(8, 8, 163333.45, 4666.67, '2023-11-09', '2026-11-09', true),
(9, 9, 169625.00, 2875.00, '2023-11-09', '2028-11-09', true),
(10, 10, 183875.00, 3125.00, '2023-11-09', '2026-11-09', true);

insert into payment(debt_id, value, method, payment_date, user_id) VALUES
(1, 2944.45, 'Debit Card', '2023-12-09', 1),
(1, 1916.67, 'Debit Card', '2023-12-09', 2),
(1, 3111.11, 'Debit Card', '2023-12-09', 3),
(1, 2083.34, 'Debit Card', '2023-12-09', 4),
(1, 2000.00, 'Debit Card', '2023-12-09', 5),
(1, 3027.78, 'Debit Card', '2023-12-09', 6),
(1, 1833.34, 'Debit Card', '2023-12-09', 7),
(1, 4666.67, 'Debit Card', '2023-12-09', 8),
(1, 2875.00, 'Debit Card', '2023-12-09', 9),
(1, 3125.00, 'Debit Card', '2023-12-09', 10);

--LOGIN QUERY - 
 SELECT * FROM user_account WHERE user_username = 'user_example' AND user_password = 'password123';

--FOR CONFIRMATION AND COMPUTATION

SELECT creditor_id, loan_type, interest FROM creditors WHERE company_name = 'creditor';


--update amount

UPDATE debt SET amount = '94,768.55' WHERE user_id = '1';

--find active loan

SELECT debt_id, first_name, last_name, company_name, loan_type, amount, debt_date, due_date FROM debt
inner join user_account
on user_account.user_id = debt.user_id
inner join creditors
on creditors.creditor_id = debt.debt_id
WHERE user_username = 'arthur_morgan' AND is_active = true;

--find settled loan

SELECT debt_id, first_name, last_name, company_name, loan_type, amount, debt_date, due_date FROM debt
inner join user_account
on user_account.user_id = debt.user_id
inner join creditors
on creditors.creditor_id = debt.debt_id
WHERE user_username = 'arthur_morgan' AND is_active = false;


--select transactions
SELECT * FROM payment WHERE user_id = '1';