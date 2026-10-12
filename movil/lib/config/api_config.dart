class ApiConfig {
  static const baseUrl = String.fromEnvironment(
    'API_BASE_URL',
    defaultValue: 'http://10.0.2.2:5080',
  );
  static Uri get correspondencias => Uri.parse('$baseUrl/api/correspondencias');
}
