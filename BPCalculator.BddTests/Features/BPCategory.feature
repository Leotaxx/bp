Feature: Blood pressure categorisation
  In order to understand my blood pressure
  As a user
  I want the app to label my blood pressure correctly

  Scenario Outline: Calculate category
    Given I enter a systolic value of <systolic>
    And I enter a diastolic value of <diastolic>
    When I compute the blood pressure category
    Then the result should be <category>

    Examples:
      | systolic | diastolic | category |
      | 150      | 95        | High     |
      | 130      | 85        | PreHigh  |
      | 85       | 55        | Low      |
      | 115      | 70        | Ideal    |
