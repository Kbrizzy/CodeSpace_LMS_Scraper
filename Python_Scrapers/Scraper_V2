import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from bs4 import BeautifulSoup
from urllib.parse import urljoin

# base url
base_url = "https://learn.codespace.co.za/login"

# initialize Selenium WebDriver
driver = webdriver.Chrome()

# login
driver.get(base_url)
wait = WebDriverWait(driver, 10)

username = wait.until(EC.presence_of_element_located((By.NAME, 'email')))
password = wait.until(EC.presence_of_element_located((By.NAME, 'password')))

username.send_keys('david@codespace.co.za')
password.send_keys('')

login_button = wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, '.button.is-primary')))
login_button.click()

time.sleep(15)  # increase sleep time

# get all links function
def get_all_links(url):
    driver.get(url)
    time.sleep(15)  # increase sleep time
    html = driver.page_source
    soup = BeautifulSoup(html, 'html.parser')
    anchors = soup.find_all('a')
    links = set()
    for anchor in anchors:
        link = anchor.get('href')
        if link and not link.startswith('javascript:;'):
            full_link = urljoin(url, link)
            links.add(full_link)
    return links

# list of course urls
course_urls = ["https://learn.codespace.co.za/courses/147"]

link_info = []

# for each course url, get all links and test them
for course_url in course_urls:
    print(f"\nGetting links for: {course_url}")
    all_links = get_all_links(course_url)
    for link in all_links:
        driver.get(link)
        time.sleep(15)  # increase sleep time

        # Check if page loaded properly
        if "Page not found" in driver.title or "Error" in driver.title:
            status = "Error"
            text_count = video_count = 0
            video_links = []
        else:
            status = "OK"
            html = driver.page_source
            soup = BeautifulSoup(html, 'html.parser')
            content_blocks = soup.find_all(class_='lesson-content-block column')

            text_count = sum(len(block.get_text().split()) for block in content_blocks)

            video_elements = soup.find_all('iframe', src=lambda x: x and 'youtube.com' in x)
            video_count = len(video_elements)
            video_links = [el['src'] for el in video_elements]

        link_info.append({
            "Link": link, 
            "Status": status, 
            "Text Count": text_count, 
            "Video Count": video_count, 
            "Video Links": video_links,
        })

# Print summary
print(f"\nTotal links: {len(all_links)}")

# Create DataFrame with link info
df = pd.DataFrame(link_info, columns=["Link", "Status", "Text Count", "Video Count", "Video Links"])
print(df)

# close WebDriver
driver.quit()
