import { test, expect } from '@playwright/test';

test('user can calculate pulse pressure and category', async ({ page }) => {
  await page.goto('/');
  await page.fill('input[name="BP.Systolic"]', '130');
  await page.fill('input[name="BP.Diastolic"]', '80');
  await page.click('text=Calculate');

  await expect(page.getByText('Category')).toBeVisible();
  await expect(page.getByText('PreHigh')).toBeVisible();
  await expect(page.getByText(/Pulse Pressure/)).toBeVisible();
  await expect(page.getByText(/50 mmHg/)).toBeVisible();
});
