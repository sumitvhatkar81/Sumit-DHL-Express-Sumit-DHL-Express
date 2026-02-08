# DHL Document App - Postman Testing Guide

## Overview
This guide provides comprehensive instructions for testing the DHL Document App API using the provided Postman collection and environment files.

## Files Included
- `DHL-Document-App-Postman-Collection.json` - Complete API test collection
- `DHL-Document-App-Environment.json` - Environment variables for local development
- `README-Postman-Testing.md` - This testing guide

## API Endpoints Covered

### Document Management
1. **GET /api/Documents** - Retrieve all documents
2. **GET /api/Documents/{id}** - Retrieve specific document by ID
3. **POST /api/Documents** - Create new document (JSON payload)
4. **POST /api/Documents/upload** - Upload document file (multipart/form-data)
5. **PUT /api/Documents/{id}** - Update existing document
6. **DELETE /api/Documents/{id}** - Delete document

### Error Scenarios
1. **GET /api/Documents/99999** - Test non-existent document (404)
2. **POST /api/Documents/upload** - Test invalid file type (400)
3. **POST /api/Documents/upload** - Test empty file upload (400)
4. **PUT /api/Documents/{id}** - Test mismatched ID update (400)

## Setup Instructions

### 1. Import Collection and Environment
1. Open Postman
2. Click **Import** button
3. Select `DHL-Document-App-Postman-Collection.json`
4. Select `DHL-Document-App-Environment.json`
5. Both files will be imported into your Postman workspace

### 2. Configure Environment
1. Select **DHL Document App - Local Development** environment
2. Verify the following variables:
   - `baseUrl`: https://localhost:7127 (HTTPS)
   - `httpBaseUrl`: http://localhost:5000 (HTTP fallback)
   - `documentId`: 1 (default test document ID)

### 3. Start the Application
Before running tests, ensure the DHL Document App is running:
```bash
cd "c:\Users\rahvhatk\Documents\Final Project\DHL-Document-App"
dotnet run
```

## Test Execution Guide

### Running Individual Tests
1. Select the **DHL Document App API Tests** collection
2. Navigate to specific requests under **Documents** or **Error Scenarios** folders
3. Click **Send** to execute individual requests
4. Review response status, headers, and body
5. Check the **Test Results** tab for automated test results

### Running Complete Test Suite
1. Right-click on **DHL Document App API Tests** collection
2. Select **Run collection**
3. Configure run settings:
   - **Iterations**: 1
   - **Delay**: 500ms between requests
   - **Data**: None required
4. Click **Run DHL Document App API Tests**
5. Monitor test execution and results

## Test Scenarios Explained

### 1. Get All Documents
- **Purpose**: Verify API can retrieve all documents
- **Tests**: Status 200, response is array, performance check
- **Variables**: Stores first document ID for subsequent tests

### 2. Get Document by ID
- **Purpose**: Test document retrieval by specific ID
- **Tests**: Status 200/404, response structure validation
- **Dependencies**: Uses `documentId` variable

### 3. Create Document (JSON)
- **Purpose**: Test document creation via JSON payload
- **Tests**: Status 201, response structure, Location header
- **Variables**: Stores created document ID for cleanup

### 4. Upload Document (File)
- **Purpose**: Test file upload functionality
- **Requirements**: Select an image file (JPEG, PNG, GIF, BMP, WebP)
- **Tests**: Status 201, upload details validation, content type check
- **Variables**: Stores uploaded document ID

### 5. Update Document
- **Purpose**: Test document modification
- **Tests**: Status 204, no content response
- **Dependencies**: Uses existing `documentId`

### 6. Delete Document
- **Purpose**: Test document deletion
- **Tests**: Status 204, no content response
- **Dependencies**: Uses `createdDocumentId` from create test

## Error Scenario Testing

### Non-Existent Document (404)
- Tests proper handling of invalid document IDs
- Expects 404 Not Found status

### Invalid File Type (400)
- Tests file type validation
- Upload non-image file to trigger validation error
- Expects 400 Bad Request with descriptive error message

### Empty File Upload (400)
- Tests empty file handling
- Expects 400 Bad Request with appropriate error

### Mismatched ID Update (400)
- Tests ID validation in PUT requests
- Expects 400 Bad Request when URL ID doesn't match body ID

## Automated Test Scripts

Each request includes JavaScript test scripts that automatically validate:
- **Status Codes**: Correct HTTP status for each scenario
- **Response Structure**: Required fields and data types
- **Performance**: Response time thresholds
- **Business Logic**: Content validation and error messages
- **Variable Management**: Dynamic ID storage and retrieval

## File Upload Testing

### Supported File Types
- JPEG (.jpg, .jpeg)
- PNG (.png)
- GIF (.gif)
- BMP (.bmp)
- WebP (.webp)

### File Size Limits
- Maximum file size: 10MB
- Files exceeding limit will return 400 Bad Request

### Upload Test Procedure
1. Navigate to **Upload Document (File)** request
2. In **Body** tab, click **Select Files** for the `file` parameter
3. Choose a valid image file from your system
4. Optionally modify the `name` parameter
5. Click **Send**
6. Verify successful upload and response structure

## Troubleshooting

### Common Issues

#### Connection Refused
- **Cause**: Application not running or wrong port
- **Solution**: Start the app with `dotnet run` and verify port in environment

#### SSL Certificate Errors
- **Cause**: HTTPS certificate issues in development
- **Solution**: Use `httpBaseUrl` variable or disable SSL verification in Postman

#### 415 Unsupported Media Type
- **Cause**: Incorrect Content-Type header for file uploads
- **Solution**: Ensure file upload requests use multipart/form-data (automatically set)

#### Test Failures
- **Cause**: Unexpected response structure or status codes
- **Solution**: Check application logs and verify API implementation

### Debug Tips
1. Enable Postman Console (View → Show Postman Console)
2. Check request/response details in console
3. Verify environment variable values
4. Review application logs for server-side errors
5. Test individual requests before running full collection

## Expected Test Results

### Successful Run Summary
- **Total Requests**: 10
- **Passed Tests**: ~30-40 individual assertions
- **Failed Tests**: 0 (in ideal scenario)
- **Average Response Time**: < 1000ms per request

### Key Performance Metrics
- GET requests: < 1000ms
- POST requests: < 2000ms
- File uploads: < 3000ms (depends on file size)
- DELETE requests: < 1000ms

## Next Steps

After successful testing:
1. **Integration Testing**: Test with real file uploads of various sizes
2. **Load Testing**: Use Postman Runner with multiple iterations
3. **Security Testing**: Test with malicious file types and oversized files
4. **API Documentation**: Generate API documentation from collection
5. **CI/CD Integration**: Integrate collection into automated testing pipeline

## Support

For issues or questions:
1. Check application logs in the console
2. Verify environment configuration
3. Review this documentation
4. Test individual endpoints manually
5. Check network connectivity and firewall settings