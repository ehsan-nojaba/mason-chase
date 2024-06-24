Feature: Customer Management
  In order to manage customers
  As an operator
  I want to be able to create, update, and delete customers

  Scenario: Operator creates a customer
    Given a customer with the following details:
      | FirstName | LastName | Email            | DateOfBirth | PhoneNumber    |
      | John      | Doe      | john@example.com | 1990-01-01  | +989123456789  |
    When the operator creates the customer
    Then the customer should be created successfully

  Scenario: Operator updates a customer
    Given a customer with the following details:
      | FirstName | LastName | Email            | DateOfBirth | PhoneNumber    |
      | John      | Doe      | john@example.com | 1990-01-01  | +989123456789  |
    When the operator updates the customer with new details:
      | FirstName | LastName | Email               | DateOfBirth | PhoneNumber    |
      | Johnny    | Doe      | johnny@example.com  | 1990-01-01  | +989123456789  |
    Then the customer should be updated successfully

  Scenario: Operator deletes a customer
    Given a customer with the following details:
      | FirstName | LastName | Email            | DateOfBirth | PhoneNumber    |
      | John      | Doe      | john@example.com | 1990-01-01  | +989123456789  |
    When the operator deletes the customer
    Then the customer should be deleted successfully
