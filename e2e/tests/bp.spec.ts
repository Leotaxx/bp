import { test, expect } from '@playwright/test';

test('user can calculate pulse pressure and category', async ({ page }) => {
  await page.goto('/', { waitUntil: 'networkidle' });
  await page.locator('input[name="BP.Systolic"]').waitFor({ timeout: 10000 });
  await page.fill('input[name="BP.Systolic"]', '130');
  await page.fill('input[name="BP.Diastolic"]', '80');
  await page.click('text=Calculate');

  const result = page.locator('.bp-result');
  await expect(result).toBeVisible();
  await expect(result.locator('.bp-label', { hasText: 'Category' })).toBeVisible();
  await expect(result.locator('.bp-value', { hasText: 'PreHigh' })).toBeVisible();
  await expect(result.locator('.bp-value', { hasText: /50 mmHg/ })).toBeVisible();
});
