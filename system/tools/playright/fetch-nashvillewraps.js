const { chromium } = require('playwright');
const fs = require('fs');
const path = require('path');

const targets = {
  wraps: 'https://www.nashvillewraps.com',
};

function parseArgs(argv) {
  const parsed = {};
  for (const arg of argv) {
    if (!arg.startsWith('--')) continue;
    const [key, value = true] = arg.replace(/^--/, '').split('=');
    parsed[key] = value;
  }
  return parsed;
}

async function main() {
  const args = parseArgs(process.argv.slice(2));
  const targetName = args.target || process.env.TARGET || 'wraps';
  const url = args.url || process.env.TARGET_URL || targets[targetName] || targets.wraps;
  const headless = (args.headless || process.env.HEADLESS || 'true') !== 'false';
  const storageState = args['storage-state'] || process.env.STORAGE_STATE;
  const saveStorageState = args['save-storage-state'] || process.env.SAVE_STORAGE_STATE;
  const outputBase = args.output || process.env.OUTPUT_DIR || path.join(__dirname, 'output', targetName);
  const screenshotPath = path.join(outputBase, `${targetName}.png`);
  const htmlPath = path.join(outputBase, `${targetName}.html`);

  fs.mkdirSync(outputBase, { recursive: true });

  const browser = await chromium.launch({
    headless,
    args: [
      '--disable-blink-features=AutomationControlled',
    ],
  });

  const context = await browser.newContext({
    userAgent:
      'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36',
    viewport: { width: 1280, height: 800 },
    storageState: storageState && fs.existsSync(storageState) ? storageState : undefined,
  });

  const page = await context.newPage();

  try {
    const response = await page.goto(url, { waitUntil: 'networkidle', timeout: 45000 });
    const status = response ? response.status() : 'no-response';

    await page.screenshot({ path: screenshotPath, fullPage: true });
    const html = await page.content();
    fs.writeFileSync(htmlPath, html);

    if (saveStorageState) {
      fs.mkdirSync(path.dirname(saveStorageState), { recursive: true });
      await context.storageState({ path: saveStorageState });
    }

    const title = (await page.title()) || '';
    console.log(
      JSON.stringify(
        {
          status,
          title,
          url,
          target: targetName,
          headless,
          screenshotPath,
          htmlPath,
          storageState: storageState || null,
          savedStorageState: saveStorageState || null,
        },
        null,
        2
      )
    );
  } catch (err) {
    console.error('Fetch failed', err);
    process.exitCode = 1;
  } finally {
    await browser.close();
  }
}

main();
