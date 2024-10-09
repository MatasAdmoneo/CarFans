import { test, expect } from '@playwright/test';
import { Session, getSession, logi } from '@auth0/nextjs-auth0';
import { generateSessionCookie, GenerateSessionCookieConfig } from '@auth0/nextjs-auth0/testing';
import { IncomingMessage, ServerResponse } from 'http';
import { Socket } from 'net';

function createMockRequestResponse() {
  const req = new IncomingMessage({} as Socket);
  const res = new ServerResponse(req);
  return { req, res };
}

const createSessionCookie = async () => {
  const { req, res } = createMockRequestResponse();

  const session = {
    user: {
      email: 'test@gmail.com',
      name: 'Test User',
      "https://CarFans.com/roles": ['User'],
    },
    expires: new Date(Date.now() + 60 * 60 * 1000), // Set expiration to 1 hour from now
  };

  await setLoginSession(req, res, { user: session.user, expires: session.expires });

  const cookie = await getSession(session, {
    secret: process.env.AUTH0_SECRET ?? '',
    user: session.user,
    expires: session.expires,
  });

  return cookie;
}

const getSessionCookie = (params: { session: Session; config: GenerateSessionCookieConfig }) => {
  const { session, config } = params;
  return generateSessionCookie(session, config);
}

const createAdvertFormData = () => {
  const date = new Date();
  date.setMonth(date.getMonth() + 1);
  return {
    brand: `BMW-${Math.random()}`,
    model: `M5-${Math.random()}`,
    manufactureYear: 2022,
    date: date.toString(),
    type: 'Engine',
    title: `Engine-${Math.random()}`,
    description: `Test description-${Math.random()}`,
    // add photo ???
  }
};

// test('Testing Auth', async ({ page }) => {
//   await page.goto('/advert');
//   // await page.getByTestId('login-button').click();
//   await page.fill('#email', 'test@gmail.com');
//   await page.fill('#password', 'Test123.');
//   await page.click('#btn-login');
  
//   let h1Element = await page.waitForSelector('h1');
//   let h1Text = await h1Element.textContent();
//   expect(h1Text).toBe("Create an advert");
// });

test('Login as a test user and create an advert', async ({ page }) => {
  await page.goto('/advert');
  // await page.getByTestId('login-button').click();
  await page.fill('#email', 'test@gmail.com');
  await page.fill('#password', 'Test123.');
  await page.click('#btn-login');
  
  // await page.getByText("Create advert").click();
  // (await page.waitForSelector('a:has-text("Create advert")')).click();
  // const createAdvert = page.locator('a:has-text("Create advert")');
  // await expect(createAdvert).toBeVisible();
  // await createAdvert.click();

  let h1Element = await page.waitForSelector('h1');
  let h1Text = await h1Element.textContent();
  expect(h1Text).toBe("Create an advert");

  let advertFormData = createAdvertFormData();

  await page.fill('input[name="brand"]', advertFormData.brand);
  await page.fill('input[name="model"]', advertFormData.model);
  await page.fill('input[name="manufactureYear"]', advertFormData.manufactureYear);
  await page.fill('#advert-date', advertFormData.date);
  await page.fill('input[name="title"]', advertFormData.title);
  await page.fill('input[name="description"]', advertFormData.description);

  const selectElement = page.locator('text="Select problem type"');
  await selectElement.click();
  const optionToSelect = page.locator('text="Engine"');
  await optionToSelect.click();

  await page.click('#create-advert');
  const toastMessage = page.locator('text="Advert successfully created"');
  await expect(toastMessage).toBeVisible();
});